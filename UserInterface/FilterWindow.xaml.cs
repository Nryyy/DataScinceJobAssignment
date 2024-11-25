using ClosedXML.Excel;
using DataManagment.Repositories;
using Microsoft.Win32;
using Models;
using System.Linq;
using System.Windows;

namespace UserInterface
{
    public partial class FilterWindow : Window
    {
        private readonly JobAssignmentRepository _jobAssignmentRepository;
        private readonly DataManagment.AppContext _context;

        public FilterWindow()
        {
            InitializeComponent();
            _context = new DataManagment.AppContext();
            _jobAssignmentRepository = new JobAssignmentRepository(_context);
            LoadFilterData();
        }

        private void LoadFilterData()
        {
            JobTitleComboBox.ItemsSource = _context.Jobs.ToList();
            EmployeeResidenceComboBox.ItemsSource = _context.Employees.ToList();
            CompanyLocationComboBox.ItemsSource = _context.Companies.ToList();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedJob = JobTitleComboBox.SelectedItem as Job;
            var selectedEmployee = EmployeeResidenceComboBox.SelectedItem as Employee;
            var selectedCompany = CompanyLocationComboBox.SelectedItem as Company;

            var filteredJobAssignments = _jobAssignmentRepository.Get()
                .Where(ja => (selectedJob == null || ja.JobId == selectedJob.Id) &&
                             (selectedEmployee == null || ja.EmployeeId == selectedEmployee.Id) &&
                             (selectedCompany == null || ja.CompanyId == selectedCompany.Id))
                .Select(ja => new
                {
                    Job = ja.Job,
                    Employee = ja.Employee,
                    Company = ja.Company,
                    ja.Job.Title,
                    ja.WorkYear,
                    ja.Salary,
                    ja.SalaryCurrency,
                    WorkSetting = ja.WorkSetting
                })
                .ToList();

            FilteredJobAssignmentsListBox.ItemsSource = filteredJobAssignments;
        }

        private void GenerateExcelReportButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredJobAssignments = FilteredJobAssignmentsListBox.ItemsSource.Cast<dynamic>().ToList();

            if (filteredJobAssignments == null || !filteredJobAssignments.Any())
            {
                MessageBox.Show("No data available to generate the report.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save Excel Report"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Job Assignments");

                    // Adding headers
                    worksheet.Cell(1, 1).Value = "Job Title";
                    worksheet.Cell(1, 2).Value = "Employee Residence";
                    worksheet.Cell(1, 3).Value = "Company Location";
                    worksheet.Cell(1, 4).Value = "Work Year";
                    worksheet.Cell(1, 5).Value = "Salary";
                    worksheet.Cell(1, 6).Value = "Salary Currency";
                    worksheet.Cell(1, 7).Value = "Work Setting";

                    // Adding data
                    for (int i = 0; i < filteredJobAssignments.Count; i++)
                    {
                        var jobAssignment = filteredJobAssignments[i];
                        worksheet.Cell(i + 2, 1).Value = jobAssignment.Job.Title;
                        worksheet.Cell(i + 2, 2).Value = jobAssignment.Employee.Residence;
                        worksheet.Cell(i + 2, 3).Value = jobAssignment.Company.Location;
                        worksheet.Cell(i + 2, 4).Value = jobAssignment.WorkYear;
                        worksheet.Cell(i + 2, 5).Value = jobAssignment.Salary;
                        worksheet.Cell(i + 2, 6).Value = jobAssignment.SalaryCurrency;
                        worksheet.Cell(i + 2, 7).Value = jobAssignment.WorkSetting?.Setting ?? "N/A"; // Null check and default value
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                }

                MessageBox.Show("Excel report generated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
