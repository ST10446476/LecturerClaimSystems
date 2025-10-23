# Lecturer Claims Management System

A comprehensive WPF application for managing lecturer claims with submission, verification, and approval workflows.

## Features Implemented

 1. Lecturer Claim Submission
-  Simple and intuitive form for submitting claims
-  Input fields for:
  - Hours worked
  - Hourly rate
  - Additional notes (optional)
   Prominent "Submit Claim" button
-  Real-time total calculation
-  Form validation with error messages
-  Clear and straightforward user flow

 2. Coordinator/Manager Verification
-  Separate view for coordinators and managers
-  Display all pending claims in organized table format
-  Shows all necessary details:
  - Claim ID
  - Lecturer name
  - Date submitted
  - Hours worked
  - Hourly rate
  - Total amount
  - Notes
  - Attached documents
-  Approve and Reject buttons for each claim
-  Real-time status updates

 3. Document Upload Support
-  File upload functionality
-  File type restrictions (.pdf, .docx, .xlsx)
-  File size limit (5MB maximum)
-  Uploaded file name display
-  Secure file storage with claim linkage
-  Upload button in claim submission form

 4. Claim Status Tracking
-  Real-time status tracking system
-  Status labels: "Pending", "Approved", "Rejected"
-  Automatic status updates when approved/rejected
-  Transparent tracking throughout approval process
-  Visible in both lecturer and coordinator views

5. System Reliability
-  Comprehensive unit tests covering:
  - Claim calculations
  - Status updates
  - Validation logic
  - Property change notifications
-  Error handling with try-catch blocks
-  User-friendly error messages
-  Global exception handling
-  Error logging to file

 6. Version Control
-  Ready for GitHub repository
-  Clear project structure
-  Descriptive code comments
-  Commit-ready codebase

## Project Structure

```
LecturerClaimsSystem/
├── MainWindow.xaml              # Main UI layout
├── MainWindow.xaml.cs           # Main application logic
├── App.xaml                     # Application resources
├── App.xaml.cs                  # Application startup & error handling
├── ClaimTests.cs                # Unit tests
├── LecturerClaimsSystem.csproj  # Project configuration
└── README.md                    # Documentation
```

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 6.0 SDK or later
- Windows 10/11

### Installation

1. **Clone or create the project:**
   ```bash
   mkdir LecturerClaimsSystem
   cd LecturerClaimsSystem
   ```

2. **Create the project structure:**
   - Copy all provided files into the project folder
   - Ensure the .csproj file is in the root directory

3. **Open in Visual Studio:**
   - Open Visual Studio
   - File → Open → Project/Solution
   - Select `LecturerClaimsSystem.csproj`

4. **Restore NuGet packages:**
   ```
   Right-click project → Restore NuGet Packages
   ```

5. **Build the solution:**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   ```

6. **Run the application:**
   ```
   Debug → Start Debugging (F5)
   ```

## Usage Guide

### For Lecturers

1. **Submit a Claim:**
   - Enter hours worked (e.g., 40)
   - Enter hourly rate (e.g., 500)
   - Add optional notes
   - Click "Upload Document" to attach supporting files
   - Click "Submit Claim" button
   - View confirmation with claim ID and total amount

2. **View Your Claims:**
   - Scroll down to see "My Claims" table
   - Check claim status (Pending/Approved/Rejected)
   - View submission dates and amounts

### For Coordinators/Managers

1. **Switch to Verification View:**
   - Click "Verify Claims" tab
   - View all pending claims

2. **Review Claims:**
   - Select a claim from the table
   - Review all details (hours, rate, notes, documents)
   - Check the total amount

3. **Approve or Reject:**
   - Select the claim
   - Click "Approve" button (green) to approve
   - Click "Reject" button (red) to reject
   - Confirmation message appears
   - Status updates immediately

## Testing

### Run Unit Tests

1. **In Visual Studio:**
   - Test → Run All Tests (Ctrl+R, A)
   - View results in Test Explorer

2. **Tests Included:**
   - Total amount calculations
   - Status updates
   - Property change notifications
   - Input validation
   - Document handling

### Manual Testing Checklist

- [ ] Submit claim with valid data
- [ ] Submit claim with invalid data (negative numbers, empty fields)
- [ ] Upload various file types (.pdf, .docx, .xlsx)
- [ ] Try uploading file > 5MB
- [ ] Approve a pending claim
- [ ] Reject a pending claim
- [ ] Verify status updates in real-time
- [ ] Check claim history display

## Error Handling

The application includes comprehensive error handling:

1. **Input Validation:**
   - Empty field detection
   - Numeric validation
   - Positive number enforcement

2. **File Upload:**
   - File size validation
   - File type restrictions
   - Error messages for invalid uploads

3. **Global Exception Handling:**
   - Catches unhandled exceptions
   - Logs errors to `error.log`
   - Displays user-friendly messages

## Design Considerations

### Color Scheme
- **Primary Blue:** #2C3E50 (Header)
- **Success Green:** #27AE60 (Approve button)
- **Danger Red:** #E74C3C (Reject button)
- **Info Blue:** #3498DB (Upload button)
- **Light Gray:** #ECF0F1 (Form background)

### User Experience
- Clean, modern interface
- Intuitive navigation with tabs
- Clear visual hierarchy
- Prominent action buttons
- Consistent spacing and alignment
- Responsive feedback for all actions

## Future Enhancements

Potential improvements for future versions:

1. **Authentication System:**
   - User login with role-based access
   - Password management
   - Session handling

2. **Database Integration:**
   - SQL Server or SQLite backend
   - Persistent data storage
   - Historical data retrieval

3. **Email Notifications:**
   - Notify lecturers of status changes
   - Send reminders to coordinators
   - Automated approval confirmations

4. **Reporting:**
   - Generate PDF reports
   - Export to Excel
   - Monthly summaries

5. **Advanced Features:**
   - Claim editing before submission
   - Batch approval
   - Search and filter capabilities
   - Audit trail

## Git Commit Strategy

Recommended commit messages:

```bash
git init
git add .
git commit -m "Initial commit: Project structure and basic setup"
git commit -m "feat: Add claim submission form with validation"
git commit -m "feat: Implement coordinator verification view"
git commit -m "feat: Add document upload functionality with size/type validation"
git commit -m "feat: Implement real-time status tracking system"
git commit -m "test: Add comprehensive unit tests for claim logic"
git commit -m "fix: Improve error handling and user feedback"
git commit -m "docs: Add README with setup and usage instructions"
```

## Troubleshooting

### Common Issues

**Issue:** Application won't start
- **Solution:** Ensure .NET 6.0 SDK is installed, rebuild solution

**Issue:** File upload fails
- **Solution:** Check file size (<5MB) and format (.pdf, .docx, .xlsx)

**Issue:** Tests not running
- **Solution:** Restore NuGet packages, rebuild test project

**Issue:** DataGrid not displaying data
- **Solution:** Check ItemsSource binding, verify data collection is populated


