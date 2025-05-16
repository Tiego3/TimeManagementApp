# TimeManagementApp

A WPF application to help students manage their study time effectively. Built with the MVVM pattern and a reusable class library (`TimeManagementLibrary`), this app allows users to:

- Add academic modules
- Record study hours
- Track self-study progress per week

## 📁 Project Structure

TimeManagementApp
 |_ TimeManagementApp/ # WPF Application
 |_ViewModels/ # MVVM ViewModel layer
 |_Views/ # XAML Views (e.g., MainWindow.xaml)
 |_Commands/ # ICommand implementations
 |_App.xaml / MainWindow.xaml

TimeManagementLibrary/ # Core logic and model definitions
  |_Module.cs
  |_StudyTimeRecord.cs

TimeManagementApp.sln
README.md

---

## ⚙️ Features

### 1. Add Modules
- Enter details: code, name, credits, class hours per week, number of weeks, and start date.
- Self-study hours are automatically calculated using the formula:
  - SelfStudyHoursPerWeek = ((Credits × 10) / Weeks) - ClassHoursPerWeek

### 2. Record Study Time
- Select a module and log the hours studied on a specific date.
- Records are saved per module.

### 3. Track Study Progress
- View remaining self-study hours for the current week based on study records.

---

## 🧠 Built With

- [.NET](https://dotnet.microsoft.com/) 6 or later
- **WPF (Windows Presentation Foundation)**
- **MVVM Pattern**
- `ObservableCollection`, `INotifyPropertyChanged`, and `ICommand` for data binding
- Custom class library: `TimeManagementLibrary`

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 6 SDK or higher

### Steps

1. **Clone the repository**
  ```bash
 git clone https://github.com/your-username/TimeManagementApp.git

2. Open the solution in Visual Studio
  TimeManagementApp.sln
3. Build and run the project



  
