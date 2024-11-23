using DataManagment.Repositories;
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
    }
}
