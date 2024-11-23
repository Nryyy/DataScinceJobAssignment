using CsvHelper;
using CsvHelper.Configuration;
using DataManagment.Helpers;
using Models;
using System.Globalization;
using System.IO;
using System.Linq;

public class CsvDataParser
{
    private readonly DataManagment.AppContext _context;

    public CsvDataParser(DataManagment.AppContext context)
    {
        _context = context;
    }

    public void ParseAndSave(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        var records = csv.GetRecords<CsvJobRecord>().ToList();

        foreach (var record in records)
        {
            // Перевірка існування або додавання JobCategory
            var jobCategory = _context.JobCategories
                .FirstOrDefault(c => c.Name == record.JobCategory);
            if (jobCategory == null)
            {
                jobCategory = new JobCategory { Name = record.JobCategory };
                _context.JobCategories.Add(jobCategory);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання ExperienceLevel
            var experienceLevel = _context.ExperienceLevels
                .FirstOrDefault(e => e.Name == record.ExperienceLevel);
            if (experienceLevel == null)
            {
                experienceLevel = new ExperienceLevel
                {
                    Name = record.ExperienceLevel,
                    Description = record.ExperienceLevel
                };
                _context.ExperienceLevels.Add(experienceLevel);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання EmploymentType
            var employmentType = _context.EmploymentTypes
                .FirstOrDefault(e => e.TypeCode == record.EmploymentType);
            if (employmentType == null)
            {
                employmentType = new EmploymentTypes
                {
                    TypeCode = record.EmploymentType,
                    Description = record.EmploymentType
                };
                _context.EmploymentTypes.Add(employmentType);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання WorkSetting
            var workSetting = _context.WorkSettings
                .FirstOrDefault(w => w.Setting == record.WorkSetting);
            if (workSetting == null)
            {
                workSetting = new WorkSetting { Setting = record.WorkSetting };
                _context.WorkSettings.Add(workSetting);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання CompanySize
            var companySize = _context.CompanySizes
                .FirstOrDefault(s => s.SizeCode == record.CompanySize);
            if (companySize == null)
            {
                companySize = new CompanySize
                {
                    SizeCode = record.CompanySize,
                    Description = record.CompanySize
                };
                _context.CompanySizes.Add(companySize);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання Companies
            var company = _context.Companies
                .FirstOrDefault(c => c.Location == record.CompanyLocation && c.SizeId == companySize.Id && c.EmploymentTypeId == employmentType.Id);
            if (company == null)
            {
                company = new Company
                {
                    Location = record.CompanyLocation,
                    SizeId = companySize.Id,
                    EmploymentTypeId = employmentType.Id
                };
                _context.Companies.Add(company);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання Employees
            var employee = _context.Employees
                .FirstOrDefault(e => e.Residence == record.EmployeeResidence && e.ExperienceLevelId == experienceLevel.Id);
            if (employee == null)
            {
                employee = new Employee
                {
                    Residence = record.EmployeeResidence,
                    ExperienceLevelId = experienceLevel.Id
                };
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }

            // Перевірка існування або додавання Jobs
            var job = _context.Jobs
                .FirstOrDefault(j => j.Title == record.JobTitle && j.CategoryId == jobCategory.Id);
            if (job == null)
            {
                job = new Job
                {
                    Title = record.JobTitle,
                    CategoryId = jobCategory.Id
                };
                _context.Jobs.Add(job);
                _context.SaveChanges();
            }

            // Додавання JobAssignments
            var jobAssignment = new JobAssignment
            {
                JobId = job.Id,
                EmployeeId = employee.Id,
                CompanyId = company.Id,
                WorkSettingId = workSetting.Id,
                WorkYear = record.WorkYear,
                SalaryCurrency = record.SalaryCurrency,
                Salary = record.Salary,
                SalaryInUSD = record.SalaryInUSD
            };
            _context.JobAssignments.Add(jobAssignment);
            _context.SaveChanges();
        }
    }
}
