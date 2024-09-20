<img src="https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF.Navigation/master/Icon.png" alt="logo" width="64"/>

# Chapter.Net.WPF.Navigation Library

## Overview
Chapter.Net.WPF.Navigation brings everything you need to open windows, close windows, use system dialogs, show messages boxes and more till an 'async constructor'. Its the perfect base and very easy to use navigation framework for any kind of application, multi or single window applications.

## Features
- **INavigationService:** The main interface which brings all together. Display dialogs, windows, user controls, calls the ILoader and IEditable. The single point of entry to do anything in regard of navigation.
- **Use alltogether or stand alone:** Objects like the IDialogProvider or IMessageBoxProvider or IWindowProvider can be used stand alone even when give into a INavigationService.
- **Browse for files and folders:** Using the IDialogProvider any system dialog, browse folder, save file, open file, can be used out of ViewModels.
- **Show message boxes:** Using the IMessageBoxProvider any kind of message boxes can be used out of ViewModels.
- **Maintain windows:** Using the IWindowProvider you can create new and get existing windows (and user controls).
- **Host single user controls:** Using the SingleNavigationPresenter out of ViewModels UserControls can be displayed and replaced on the UI.
- **Host stacked user controls:** Using the StackedNavigationPresenter out of ViewModels UserControls can be added and removed on the UI.
- **Async ctor:** Using the INavigationService each ViewModel can implement the ILoader to have an async constructor in the ViewModel, called each time its UI element gets shown.
- **Leave prevention:** Using the INavigationService each ViewModel can implement the IEditable to get informed if the user tries to close a window and it can be prevented like user have to save changes first and stuff like that.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.WPF.Navigation library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.WPF.Navigation
    ```

2. **INavigationService:**
    ```csharp
    public void Bootstrapper
    {
        IUnityContainer _unityContainer;

        public Bootstrapper()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterSingleton<IWindowProvider, WindowProvider>();
            _unityContainer.RegisterSingleton<INavigationPresenterProvider, NavigationPresenterProvider>();
            _unityContainer.RegisterType<IDialogProvider, DialogProvider>();
            _unityContainer.RegisterType<IMessageBoxProvider, MessageBoxProvider>();
            _unityContainer.RegisterType<INavigationService, NavigationService>();

            RegisterViews();
        }

        public void RegisterViews()
        {
            var windowProvider = (WindowProvider)_unityContainer.Resolve<IWindowProvider>();
        
            windowProvider.RegisterWindow<MainView>("MainView");
            windowProvider.RegisterWindow<SubView>("SubView");
        
            windowProvider.RegisterControl<DialogsView>("DialogsView");
            windowProvider.RegisterControl<DisplayControlView>("DisplayControlView");
        }
    }
    ```
    ```csharp
    public class WindowViewModel : ObservableObject, ILoader
    {
        public async Task Load()
        {
            // Loads the data as soon the window got shown.
            await Task.CompletedTask;
        }
    }
    ```
    ```csharp
    public void ViewModel : ObservableObject
    {
        private INavigationService _navigationService;

        public ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task ShowWindow()
        {
            var vm = new WindowViewModel();
            await _navigationService.ShowModalWindow("MainView", vm);
        }
    }
    ```
    ```csharp
    [TestFixture]
    public class ViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private ViewModel _target;

        [SetUp]
        public void Setup()
        {
            _navigationService = new Mock<INavigationService>();
            _target = new ViewModel(_navigationService.Object);
        }

        [Test]
        public void ShowWindow_Called_ShowsTheWindow()
        {
            _target.ShowWindow();

            _navigationService.Verify(x => x.ShowModalWindow(Args.Any<string>(), Args.Any<object>()), Times.Once);
        }
    }
    ```

3. **Browse for files and folders:**
    ```csharp
    public class ViewModel : ObservableObject
    {
        private IDialogProvider _dialogProvider;

        public ViewModel(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
        }

        public string GetFile()
        {
            var data = new OpenFileData
            {
                CheckFileExists = true,
                MultiSelect = false
            };

            if (_dialogProvider.Show(data))
                return data.FileName;

            return null;
        }
    }
    ```
    ```csharp
    [TestFixture]
    public class ViewModelTests
    {
        private Mock<IDialogProvider> _dialogProvider;
        private ViewModel _target;

        [SetUp]
        public void Setup()
        {
            _dialogProvider = new Mock<IDialogProvider>();
            _target = new ViewModel(_dialogProvider.Object);
        }

        [Test]
        public void GetFile_Called_ReturnsTheUserSelectedFile()
        {
            _dialogProvider.Setup(x => x.Show(Arg.Any<IOpenFileData>())
                .Callback(e => e.FileName = "filename")
                .Returns(true);

            var result = _target.GetFile();

            Assert.That(result, Is.EqualTo("filename"));
        }
    }
    ```

4. **Show message boxes:**
    ```csharp
    public class ViewModel : ObservableObject
    {
        private IMessageBoxProvider _messageBoxProvider;

        public ViewModel(IMessageBoxProvider messageBoxProvider)
        {
            _messageBoxProvider = messageBoxProvider;
        }

        public bool ShallDeleteFile()
        {
            var result = _messageBoxProvider.Show("Do you want to delete the file?", "Question", MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
    }
    ```
    ```csharp
    public class ViewModelTests
    {
        private Mock<IMessageBoxProvider> _messageBoxProvider;
        private ViewModel _target;

        [SetUp]
        public void Setup()
        {
            _messageBoxProvider = new Mock<IMessageBoxProvider>();
            _target = new ViewModel(_messageBoxProvider.Object);
        }

        [Test]
        public void ShallDeleteFile_UserConfirmed_ReturnsTrue()
        {
            _messageBoxProvider.Setup(x => x.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>())
                .Returns(MessageBoxResult.Yes);

            var result = _target.ShallDeleteFile();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ShallDeleteFile_UserRejected_ReturnsTrue()
        {
            _messageBoxProvider.Setup(x => x.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>())
                .Returns(MessageBoxResult.No);

            var result = _target.ShallDeleteFile();

            Assert.That(result, Is.False);
        }
    }
    ```

5. **Maintain windows:**
    ```csharp
    public class Bootstrapper
    {
        public Bootstrapper()
        {
            _container = new UnityContainer();
            _container.RegisterSingleton<IWindowProvider, WindowProvider>();
        }

        public void Startup()
        {
            var provider = (WindowProvider)_container.Resolve<IWindowProvider>();
            provider.Register<MainView>("MainView");
        }
    }
    ```
    ```csharp
    public class ViewModel : ObservableObject
    {
        private IWindowProvider _windowProvider;

        public ViewModel(IWindowProvider windowProvider)
        {
            _windowProvider = windowProvider;
        }

        public void Show(object viewModel)
        {
            var window = _windowProvider.GetNewWindow("MainView");
            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
    ```

6. **Host single user controls:**
    ```xaml
    <Window>
        <chapter:SingleNavigationPresenter ID="MainSpot" />
    </Window>
    ```
    ```csharp
    public class ViewModel : ObservableObject
    {
        private INavigationService _navigationService;

        public ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task SwitchAsync()
        {
            var vm = new ControlViewModel();
            // "Control" is the user control registered in the IWindowService for the INavigationService.
            await _navigationService.ShowControl("MainSpot", "Control", vm);
        }
    }
    ```

7. **Host stacked user controls:**
    ```xaml
    <Window>
        <chapter:StackedNavigationPresenter Strategy="Overlapping" ID="MainSpot" />
    </Window>
    ```
    ```csharp
    public class ViewModel : ObservableObject
    {
        private INavigationService _navigationService;

        public ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task SwitchAsync()
        {
            var vm = new ControlViewModel();
            // "Control" is the user control registered in the IWindowService for the INavigationService.
            await _navigationService.ShowControl("MainSpot", "Control", vm);
        }
    }
    ```

8. **Async ctor:**
    ```csharp
    public class WindowViewModel : ObservableObject, ILoader
    {
        public async Task Load()
        {
            // Loads the data as soon the window got shown.
            await Task.CompletedTask;
        }
    }
    ```

9. **Leave prevention:**
    ```csharp
    public class WindowViewModel : ObservableObject, IEditable
    {
        public async Task<bool> TryLeave()
        {
            // Check for data be saved.
            await Task.CompletedTask;
            return true;
        }
    }
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF.Navigation)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF.Navigation)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
