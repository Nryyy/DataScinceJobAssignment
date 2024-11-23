using DataManagment.Repositories;
using Models;
using System;
using System.Linq;
using System.Windows;

namespace UserInterface
{
    public partial class JobManagementWindow : Window
    {
        private readonly JobRepository _jobRepository;
        private readonly DataManagment.AppContext _context;

        public JobManagementWindow()
        {
            InitializeComponent();
            _context = new DataManagment.AppContext();
            _jobRepository = new JobRepository(_context);
            LoadJobs();
            LoadJobCategories();
        }

        private void LoadJobs()
        {
            JobsListBox.ItemsSource = _jobRepository.Get().ToList();
        }

        private void LoadJobCategories()
        {
            JobCategoryListBox.ItemsSource = _context.JobCategories.ToList();
        }

        private void AddJobButton_Click(object sender, RoutedEventArgs e)
        {
            var jobTitle = JobTitleTextBox.Text;
            var selectedCategories = JobCategoryListBox.SelectedItems.Cast<JobCategory>().ToList();

            if (string.IsNullOrEmpty(jobTitle) || !selectedCategories.Any())
            {
                MessageBox.Show("Please enter a job title and select at least one job category.");
                return;
            }

            foreach (var category in selectedCategories)
            {
                var job = new Job
                {
                    Title = jobTitle,
                    CategoryId = category.Id,
                    Category = category
                };

                _jobRepository.Create(job);
            }
            LoadJobs();
        }

        private void UpdateJobButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobsListBox.SelectedItem is Job selectedJob)
            {
                var jobTitle = JobTitleTextBox.Text;
                var selectedCategories = JobCategoryListBox.SelectedItems.Cast<JobCategory>().ToList();

                if (string.IsNullOrEmpty(jobTitle) || !selectedCategories.Any())
                {
                    MessageBox.Show("Please enter a job title and select at least one job category.");
                    return;
                }

                selectedJob.Title = jobTitle;
                selectedJob.CategoryId = selectedCategories.First().Id; // Assuming one category for simplicity
                selectedJob.Category = selectedCategories.First();

                _jobRepository.Update(selectedJob);
                LoadJobs();
            }
            else
            {
                MessageBox.Show("Please select a job to update.");
            }
        }

        private void DeleteJobButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobsListBox.SelectedItem is Job selectedJob)
            {
                _jobRepository.Delete(selectedJob.Id);
                LoadJobs();
            }
            else
            {
                MessageBox.Show("Please select a job to delete.");
            }
        }
    }
}
