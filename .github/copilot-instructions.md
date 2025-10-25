# MauiLocalDB AI Coding Instructions

This is a cross-platform .NET MAUI application demonstrating local SQLite database operations with a clean MVVM architecture. The app manages projects, tasks, categories, and tags with visual components and data persistence.

## Architecture Overview

**MVVM Pattern**: Uses CommunityToolkit.Mvvm with `ObservableObject` base classes and `[ObservableProperty]` attributes for automatic property change notification. Page models are in `PageModels/` and inherit from `ObservableObject`.

**Repository Pattern**: Data access through dedicated repositories (`ProjectRepository`, `TaskRepository`, `CategoryRepository`, `TagRepository`) that handle SQLite operations using `Microsoft.Data.Sqlite`. All repositories use raw SQL queries, not an ORM.

**Dependency Injection**: Services registered in `MauiProgram.cs`. Repositories and page models are singletons, detail page models are transient with shell routing. Use constructor injection throughout.

## Key Patterns & Conventions

### Data Layer
- SQLite database file: `AppSQLite.db3` in `FileSystem.AppDataDirectory`
- Connection string defined in `Data/Constants.cs`
- Repositories use `_hasBeenInitialized` flag pattern for table creation
- Models in `Models/` use JSON serialization attributes for seed data loading
- Seed data loaded from `Resources/Raw/SeedData.json` via `SeedDataService`

### UI & Navigation
- Shell-based navigation with routes in `AppShell.xaml`
- Custom controls in `Pages/Controls/` (e.g., `ProjectCardView`, `TaskView`, `TagView`)
- Syncfusion components used for advanced UI (segmented control, charts)
- FluentUI icons referenced via `FluentUI.FontFamily` (defined in `Resources/Fonts/FluentUI.cs`)

### Custom Controls Architecture
- **Reusable Card Components**: `ProjectCardView`, `TaskView`, `TagView` are self-contained Border controls with data binding
- **Shimmer Loading Pattern**: All major controls wrap content in `SfShimmer` with custom loading views that mirror actual content structure
- **Data Template Selection**: `ChipDataTemplateSelector` demonstrates conditional UI based on model state (e.g., selected vs normal tags)
- **Syncfusion Integration**: Controls leverage `SfEffectsView` for touch feedback, `SfCircularChart` for data visualization
- **Theme-Aware Styling**: Controls use `AppThemeBinding` for light/dark theme support and reference centralized styles
- **Responsive Design**: `OnIdiom` and `OnPlatform` markup extensions handle cross-platform differences (padding, sizing)
- **Binding Context**: Controls expect specific model types via `x:DataType` for compile-time binding validation

### Error Handling
- `ModalErrorHandler` service for centralized error display via Shell alerts
- `FireAndForgetSafeAsync()` extension method for async operations without blocking UI
- Semaphore-based protection in error handler to prevent multiple simultaneous alerts

### Code Organization
- `GlobalUsings.cs` provides project-wide namespace imports
- Page models implement `IProjectTaskPageModel` interface for common project/task operations
- Platform-specific code in `Platforms/` (MacCatalyst, Windows)
- Custom utilities in `Utilities/` for common operations

## Development Guidelines

**Building**: Standard .NET MAUI project targeting `net9.0-maccatalyst` and `net9.0-windows10.0.19041.0`

**Dependencies**: 
- CommunityToolkit.Mvvm for MVVM infrastructure
- CommunityToolkit.Maui for enhanced controls
- Syncfusion.Maui.Toolkit for premium UI components
- Microsoft.Data.Sqlite.Core + SQLitePCLRaw for database access

**Testing New Features**: Use seed data service to populate sample data for testing UI components and data flows.

When adding new features:
1. Create model classes with JSON attributes if they need seed data
2. Add repository methods using raw SQL queries
3. Register new services in `MauiProgram.cs`
4. Follow MVVM pattern with `ObservableProperty` attributes
5. Use `FireAndForgetSafeAsync()` for non-blocking async operations