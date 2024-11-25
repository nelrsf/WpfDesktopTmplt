# Modern WPF Application Template

A sophisticated Windows Presentation Foundation (WPF) application template built with best practices, following MVVM pattern, and incorporating modern UI design principles. This template provides a solid foundation for building professional desktop applications with docking windows, ribbon controls, and modular architecture.

## 🌟 Features

### Core Architecture
- **MVVM Pattern Implementation**
- **Dependency Injection** using Microsoft.Extensions.DependencyInjection
- **Mediator Pattern** for loose coupling between components
- **Command Pattern** for window management
- **Interface-based Design** for better testability and maintenance

### User Interface Components
- **Modern Ribbon Interface** with customizable tabs and commands
- **Docking Window System** using AvalonDock
  - Supports multiple document interface (MDI)
  - Flexible window arrangements (vertical, horizontal, cascade, grid)
  - Dockable side panels
- **Responsive Layout Design**
- **Theme Support** with consistent styling

### Pre-built Modules
1. **Welcome Screen**
   - Quick access to recent projects
   - New project creation
   - Project opening interface

2. **Dashboard Module**
   - KPI display
   - Data visualization components
   - Recent activity tracking

3. **Calendar Module**
   - Event management
   - Date navigation
   - Event categorization

4. **Notifications System**
   - Real-time notification display
   - Priority-based categorization
   - Interactive notification handling

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or later

### Installation

1. Clone the repository:
```bash
git clone https://github.com/nelrsf/WpfDesktopTmplt.git
```

2. Open the solution in Visual Studio 2022

3. Restore NuGet packages:
```bash
dotnet restore
```

4. Build the solution:
```bash
dotnet build
```

### Project Structure

```
WpfApp2/
├── Core/
│   ├── Application/
│   │   ├── Interfaces/         # Core interfaces
│   │   └── Services/          # Application services
│   └── Entities/              # Business entities
├── Infrastructure/
│   ├── Commands/              # Command implementations
│   ├── DependencyInjection/   # DI configuration
│   └── Services/              # Infrastructure services
├── Presentation/
│   ├── Controls/              # UI controls
│   └── ViewModels/            # MVVM ViewModels
└── Resources/                 # Application resources
```

## 🛠 Customization Guide

### Adding New Windows Layout Commands

1. Create a new command class in `Infrastructure/Commands/Window`:
```csharp
public class NewLayoutCommand : ArrangeCommandBase, ICommand
{
    public event EventHandler? CanExecuteChanged;
    
    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (!(parameter is MainWindow mainWindow))
            return;

        // Implement your layout logic here
    }
}
```

2. Register the command in `CommandDictionary.cs`:
```csharp
public class CommandDictionary : ICommandDictionary
{
    public static readonly int NewCommand = 4; // Add new constant
    
    private void initializeCommands()
    {
        dictionary.Add(NewCommand, new NewLayoutCommand());
    }
}
```

### Adding New Modules

1. Create a new UserControl in `Presentation/Controls`:
```xaml
<UserControl x:Class="WpfApp2.Presentation.Controls.NewModule"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Grid>
        <!-- Your module content here -->
    </Grid>
</UserControl>
```

2. Add the module to MainWindow.xaml:
```xaml
<avalondock:LayoutDocument Title="New Module" CanClose="True">
    <controls:NewModule/>
</avalondock:LayoutDocument>
```

## 📦 Dependencies

- **AvalonDock** (4.72.1) - Docking window framework
- **Microsoft.Extensions.DependencyInjection** (8.0.1) - Dependency injection container

## 🤝 Contributing

We welcome contributions from the community! Here's how you can help:

### How to Contribute
1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Contribution Guidelines
- Follow the existing code style and conventions
- Write clean, maintainable code
- Add comments and documentation as needed
- Test your changes thoroughly
- Update README if needed
- Be respectful and constructive in discussions

### Bug Reports
If you find a bug, please create an issue that includes:
- Clear description of the problem
- Steps to reproduce
- Expected behavior
- Screenshots if applicable
- System/environment information

## 📝 License and Copyright

Copyright © 2024 MSV Technology

This software and associated documentation files are proprietary and protected by copyright law. All rights are reserved by MSV Technology.

Permission is granted to view and fork this repository for personal evaluation and non-commercial learning purposes only. No part of this software may be reproduced, distributed, or transmitted in any form or by any means for commercial purposes without the prior written permission of MSV Technology.

For licensing inquiries or permission requests, please contact us


## 🏢 About MSV Technology

MSV Technology is a leading software development company specializing in creating innovative solutions and contributing to the developer community. We are committed to:

- Building high-quality software solutions
- Promoting best practices in software development
- Supporting the developer community
- Driving technological innovation

## 🎯 Roadmap

- [ ] Dark theme support
- [ ] Localization system
- [ ] Plugin architecture
- [ ] State persistence
- [ ] Unit test coverage
- [ ] Performance optimizations

## 🙋‍♂️ Support

For support:
1. Check our documentation
2. Search existing issues
3. Create a new issue
4. Contact MSV Technology support team

## 🌟 Acknowledgments

- Development Team at MSV Technology
- [AvalonDock team for the docking framework](https://github.com/Dirkster99/AvalonDock)


---

**Note**: This template is designed to be a starting point for WPF applications. While we encourage learning from and referencing this project, please respect our copyright and licensing terms.
