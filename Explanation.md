```markdown
### Step-by-Step Guide with Code

#### 1. Set Up Your Project
   - Create a new **WPF App** project in Visual Studio.
   - Install the **`CommunityToolkit.Mvvm`** NuGet package.
     - Right-click on your project > Manage NuGet Packages > Browse > Search for `CommunityToolkit.Mvvm` and install it.

#### 2. Create a Separate Folder for ViewModels
   - Inside your project, create a new folder called `ViewModels`.
   - Inside `ViewModels`, create two new C# classes: `MainViewModel.cs` and `FileViewModel.cs`.

#### 3. Set Up `MainViewModel.cs`
   - In `MainViewModel.cs`, inherit from `ObservableObject` from the `CommunityToolkit.Mvvm.ComponentModel` namespace.
   - Define properties and commands (like `FileContent` and `SaveCommand`), and implement the logic for saving files.

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;

public class MainViewModel : ObservableObject
{
    private string fileContent;
    private string filePath = "default.txt"; // Default file path

    public RelayCommand SaveCommand { get; }

    public string FileContent
    {
        get => fileContent;
        set => SetProperty(ref fileContent, value); // Notifies the UI when content changes
    }

    public MainViewModel()
    {
        SaveCommand = new RelayCommand(SaveFile); // Bind SaveCommand to SaveFile method
    }

    private void SaveFile()
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, FileContent); // Saves content to the file
        }
        else
        {
            // Handle case where file path is not set or prompt the user to select a file
        }
    }
}
```

#### 4. Set Up `FileViewModel.cs`
   - In `FileViewModel.cs`, inherit from `ObservableObject`. This ViewModel can handle file-related properties like `FileName` and `FilePath`.

```csharp
using CommunityToolkit.Mvvm.ComponentModel;

public class FileViewModel : ObservableObject
{
    private string fileName;
    private string filePath;

    public string FileName
    {
        get => fileName;
        set => SetProperty(ref fileName, value); // Notify the UI when file name changes
    }

    public string FilePath
    {
        get => filePath;
        set => SetProperty(ref filePath, value); // Notify the UI when file path changes
    }
}
```

#### 5. Bind `MainViewModel` to `MainWindow`
   - In `MainWindow.xaml.cs`, set the `DataContext` to `MainViewModel`. This links the UI to the ViewModel so that data binding works properly.

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(); // Bind the ViewModel to the View
    }
}
```

#### 6. Update `MainWindow.xaml` for Data Binding
   - In `MainWindow.xaml`, bind UI elements (like a `TextBox` for file content and a `Button` for saving) to properties and commands from `MainViewModel`.

```xml
<Window x:Class="YourApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="SuperPad" Height="350" Width="525">

    <Grid>
        <TextBox Text="{Binding FileContent}" Height="200" Width="400" Margin="10"/>
        <Button Content="Save" Command="{Binding SaveCommand}" Height="30" Width="100" VerticalAlignment="Bottom" HorizontalAlignment="Right" Margin="10"/>
    </Grid>
</Window>
```

#### 7. Test the Application
   - Run your application. Try typing into the `TextBox` and clicking the **Save** button. Check if the file gets saved with the content you entered.

---

### Detailed Summary with Code Snippets and Explanations

#### 1. ObservableObject and MVVM Structure
   - **`ObservableObject`**: It simplifies the process of implementing the `INotifyPropertyChanged` interface. It automatically raises property change notifications when a property is updated, so the UI knows when to refresh.
   
   - In the **MVVM pattern**:
     - **View (MainWindow.xaml)**: Defines the UI (user interface).
     - **ViewModel (MainViewModel.cs & FileViewModel.cs)**: Manages the logic and data, binds to the view via data binding.
     - **Model**: Represents the data and business logic (you might not need many models for this project, but they exist for larger projects).

#### 2. Setting Up the Project
   - Create the WPF project and install the `CommunityToolkit.Mvvm` NuGet package.
   - Set up separate ViewModel classes (`MainViewModel.cs` and `FileViewModel.cs`) to manage the logic.
   - Organize your ViewModels in a separate folder for cleaner structure.

#### 3. Binding ViewModels to the View
   - In the **MVVM pattern**, the ViewModel communicates with the View through **data binding**. This allows you to link properties in the ViewModel to UI elements in the XAML.
   
   Example:
   - If the user types into a `TextBox` bound to `FileContent`, it updates the `FileContent` property in the ViewModel automatically.
   
   **XAML Example**:
   ```xml
   <TextBox Text="{Binding FileContent}" />
   ```

#### 4. Commands and Actions
   - **RelayCommand**: Used to handle user actions like clicking a button (without needing code-behind in `MainWindow.xaml.cs`).
   
   Example:
   - The **Save** button is bound to the `SaveCommand` in the ViewModel. When clicked, the `SaveFile` method is executed.
   
   **Code Example**:
   ```csharp
   public MainViewModel()
   {
       SaveCommand = new RelayCommand(SaveFile);
   }
   ```

#### 5. How `ObservableObject` and `SetProperty` Work
   - **`ObservableObject`**: Automatically implements `INotifyPropertyChanged`, which is essential for notifying the UI when properties change.
   - **`SetProperty` Method**:
     - Checks if the value has changed.
     - If it has, it updates the value and raises the `PropertyChanged` event.

   **Code Example**:
   ```csharp
   public string FileContent
   {
       get => fileContent;
       set => SetProperty(ref fileContent, value); // Notifies the UI when content changes
   }
   ```

#### 6. Save Logic
   - The save logic is in `MainViewModel`. When the user clicks **Save**, the content of the `FileContent` property is saved to a file.
   
   **Save Logic Example**:
   ```csharp
   private void SaveFile()
   {
       if (!string.IsNullOrEmpty(filePath))
       {
           File.WriteAllText(filePath, FileContent); // Saves content to file
       }
   }
   ```

#### 7. Minimal Code in `MainWindow.xaml.cs`
   - In the **MVVM pattern**, you aim to minimize code-behind in `MainWindow.xaml.cs`. The only code required here is setting the `DataContext` to `MainViewModel`, which connects the ViewModel to the View.

   **Example**:
   ```csharp
   public MainWindow()
   {
       InitializeComponent();
       DataContext = new MainViewModel(); // Link ViewModel to View
   }
   ```

#### 8. How `INotifyPropertyChanged` Works
   - **`INotifyPropertyChanged`**: This interface allows a class to notify the UI when a property value changes, prompting the UI to refresh and display the updated value.
   - **`ObservableObject`** implements `INotifyPropertyChanged` automatically. It provides the `SetProperty` method, so you don’t have to manually raise the `PropertyChanged` event for each property.

   **Example Without `ObservableObject`**:
   ```csharp
   public class MainViewModel : INotifyPropertyChanged
   {
       private string fileContent;

       public string FileContent
       {
           get => fileContent;
           set
           {
               if (fileContent != value)
               {
                   fileContent = value;
                   OnPropertyChanged(nameof(FileContent));
               }
           }
       }

       public event PropertyChangedEventHandler PropertyChanged;

       protected void OnPropertyChanged(string propertyName)
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       }
   }
   ```

#### 9. Folder Structure
   - Organize your project by creating a `ViewModels` folder for `MainViewModel.cs`, `FileViewModel.cs`, and any other ViewModel classes. This keeps your project neat and scalable.

---

### What Do `RelayCommand`s Do?

**RelayCommand** is a class used to represent commands in the **MVVM** pattern. In a typical WPF application, you can bind UI elements, such as buttons, to commands in your ViewModel rather than handling events in the code-behind.

A `RelayCommand` is designed to:
- **Encapsulate an Action**: When you create a `RelayCommand`, you pass an action (or a method) that gets executed when