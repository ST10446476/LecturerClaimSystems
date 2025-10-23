using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace LecturerClaimSystems;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
     
{
    private ObservableCollection<Claim> allClaims;
    private string currentUserRole = "Lecturer"; // Can be changed to "Coordinator" or "Manager"
    private string uploadedFilePath = "";
    private int claimIdCounter = 1;
    public MainWindow()
    {
        InitializeComponent();
        InitializeSystem();
    }

    private void InitializeSystem()
    {
        allClaims = new ObservableCollection<Claim>();

        // Set user role (in real app, this would come from login)
        SetUserRole(currentUserRole);

        // Add sample data for testing
        LoadSampleData();

        RefreshGrids();
    }

    private void SetUserRole(string role)
    {
        currentUserRole = role;
        UserRoleText.Text = $"Role: {role}";

        // Show/hide tabs based on role
        if (role == "Lecturer")
        {
            LecturerTab.Visibility = Visibility.Visible;
            CoordinatorTab.Visibility = Visibility.Collapsed;
        }
        else if (role == "Coordinator" || role == "Manager")
        {
            LecturerTab.Visibility = Visibility.Visible;
            CoordinatorTab.Visibility = Visibility.Visible;
            MainTabControl.SelectedItem = CoordinatorTab;
        }
    }

    private void LoadSampleData()
    {
        // Add sample claims for demonstration
        allClaims.Add(new Claim
        {
            ClaimId = claimIdCounter++,
            LecturerName = "Dr. John Smith",
            HoursWorked = 40,
            HourlyRate = 500,
            Notes = "Undergraduate Programming course lectures",
            SubmissionDate = DateTime.Now.AddDays(-2),
            Status = "Pending",
            DocumentName = "timesheet.pdf"
        });

        allClaims.Add(new Claim
        {
            ClaimId = claimIdCounter++,
            LecturerName = "Prof. Sarah Johnson",
            HoursWorked = 35,
            HourlyRate = 600,
            Notes = "Research methodology workshops",
            SubmissionDate = DateTime.Now.AddDays(-1),
            Status = "Pending",
            DocumentName = "workshop_schedule.docx"
        });
    }

    private void UploadButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Supporting Document",
                Filter = "Documents (*.pdf;*.docx;*.xlsx)|*.pdf;*.docx;*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(filePath);

                // Check file size (5MB limit)
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("File size exceeds 5MB limit. Please select a smaller file.",
                                  "File Too Large", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                uploadedFilePath = filePath;
                UploadedFileText.Text = fileInfo.Name;
                UploadedFileText.Foreground = System.Windows.Media.Brushes.Green;

                MessageBox.Show($"File '{fileInfo.Name}' uploaded successfully!",
                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error uploading file: {ex.Message}",
                          "Upload Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Validate inputs
            if (!ValidateClaimInputs(out double hours, out double rate))
                return;

            // Create new claim
            Claim newClaim = new Claim
            {
                ClaimId = claimIdCounter++,
                LecturerName = "Current User", // In real app, get from logged-in user
                HoursWorked = hours,
                HourlyRate = rate,
                Notes = NotesTextBox.Text.Trim(),
                SubmissionDate = DateTime.Now,
                Status = "Pending",
                DocumentName = string.IsNullOrEmpty(uploadedFilePath) ? "No document" : Path.GetFileName(uploadedFilePath),
                DocumentPath = uploadedFilePath
            };

            // Add to collection
            allClaims.Add(newClaim);

            // Show success message
            MessageBox.Show($"Claim submitted successfully!\n\n" +
                          $"Claim ID: {newClaim.ClaimId}\n" +
                          $"Total Amount: R{newClaim.TotalAmount:F2}\n" +
                          $"Status: {newClaim.Status}",
                          "Claim Submitted", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form
            ClearClaimForm();

            // Refresh grids
            RefreshGrids();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error submitting claim: {ex.Message}",
                          "Submission Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateClaimInputs(out double hours, out double rate)
    {
        hours = 0;
        rate = 0;

        // Validate hours worked
        if (string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text))
        {
            MessageBox.Show("Please enter the hours worked.",
                          "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            HoursWorkedTextBox.Focus();
            return false;
        }

        if (!double.TryParse(HoursWorkedTextBox.Text, out hours) || hours <= 0)
        {
            MessageBox.Show("Please enter a valid positive number for hours worked.",
                          "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            HoursWorkedTextBox.Focus();
            return false;
        }

        // Validate hourly rate
        if (string.IsNullOrWhiteSpace(HourlyRateTextBox.Text))
        {
            MessageBox.Show("Please enter the hourly rate.",
                          "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            HourlyRateTextBox.Focus();
            return false;
        }

        if (!double.TryParse(HourlyRateTextBox.Text, out rate) || rate <= 0)
        {
            MessageBox.Show("Please enter a valid positive number for hourly rate.",
                          "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            HourlyRateTextBox.Focus();
            return false;
        }

        return true;
    }

    private void ClearClaimForm()
    {
        HoursWorkedTextBox.Clear();
        HourlyRateTextBox.Clear();
        NotesTextBox.Clear();
        uploadedFilePath = "";
        UploadedFileText.Text = "No file selected";
        UploadedFileText.Foreground = System.Windows.Media.Brushes.Gray;
    }

    private void RefreshGrids()
    {
        // Refresh lecturer's own claims
        var lecturerClaims = allClaims.Where(c => c.LecturerName == "Current User").ToList();
        LecturerClaimsGrid.ItemsSource = null;
        LecturerClaimsGrid.ItemsSource = lecturerClaims;

        // Refresh pending claims for coordinators/managers
        var pendingClaims = allClaims.Where(c => c.Status == "Pending").ToList();
        PendingClaimsGrid.ItemsSource = null;
        PendingClaimsGrid.ItemsSource = pendingClaims;
    }

    private void PendingClaimsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Enable/disable approve/reject buttons based on selection
        bool hasSelection = PendingClaimsGrid.SelectedItem != null;
        ApproveButton.IsEnabled = hasSelection;
        RejectButton.IsEnabled = hasSelection;
    }

    private void ApproveButton_Click(object sender, RoutedEventArgs e)
    {
        if (PendingClaimsGrid.SelectedItem is Claim selectedClaim)
        {
            try
            {
                selectedClaim.Status = "Approved";
                selectedClaim.ProcessedDate = DateTime.Now;

                MessageBox.Show($"Claim ID {selectedClaim.ClaimId} has been approved!\n\n" +
                              $"Lecturer: {selectedClaim.LecturerName}\n" +
                              $"Amount: R{selectedClaim.TotalAmount:F2}",
                              "Claim Approved", MessageBoxButton.OK, MessageBoxImage.Information);

                RefreshGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving claim: {ex.Message}",
                              "Approval Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a claim to approve.",
                          "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void RejectButton_Click(object sender, RoutedEventArgs e)
    {
        if (PendingClaimsGrid.SelectedItem is Claim selectedClaim)
        {
            try
            {
                selectedClaim.Status = "Rejected";
                selectedClaim.ProcessedDate = DateTime.Now;

                MessageBox.Show($"Claim ID {selectedClaim.ClaimId} has been rejected.\n\n" +
                              $"Lecturer: {selectedClaim.LecturerName}\n" +
                              $"Amount: R{selectedClaim.TotalAmount:F2}",
                              "Claim Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);

                RefreshGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting claim: {ex.Message}",
                              "Rejection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a claim to reject.",
                          "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

// Claim Model Class
public class Claim : System.ComponentModel.INotifyPropertyChanged
{
    private string status;

    public int ClaimId { get; set; }
    public string LecturerName { get; set; }
    public double HoursWorked { get; set; }
    public double HourlyRate { get; set; }
    public string Notes { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string DocumentName { get; set; }
    public string DocumentPath { get; set; }

    public string Status
    {
        get => status;
        set
        {
            status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public double TotalAmount => HoursWorked * HourlyRate;

    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
