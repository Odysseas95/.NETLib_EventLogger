# .NETLib EventLogger

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The .NETLib EventLogger is a .NET Library designed to provide logging capabilities for various events and errors. It includes a `Logger` class that enables developers to log events of different types, such as errors, warnings, successful operations, and workarounds. The library allows logging to text files and supports email notifications for error reporting. With easy integration into .NET projects, developers can efficiently track and manage events within their applications.

## Installation

To use this library in your project, you can add the appropriate reference to your .NET project. Please follow the steps below:

1. Clone the repository to your local machine.
2. Build the project to generate the DLL file.
3. Add a reference to the generated DLL file in your project.

## Usage

The `Logger` class in this library allows you to log events and errors to text files. Here's how you can use it:

```csharp
using Email_Parser;
using static Email_Parser.Logger;

namespace YourNamespace
{
    public class YourClass
    {
        public static Logger Logger;

        public YourClass()
        {
            Logger = new Logger();

            try
            {
                // Log events with different types
                Logger.LogEvent("Im a log string for an Error", LoggerType.Error);
                Logger.LogEvent("Im a log string for a Warning", LoggerType.Warning);
                Logger.LogEvent("Im a log string for a Successful operation", LoggerType.Success);
                Logger.LogEvent("Im a log string for a Workaround taken place", LoggerType.Workaround);
            }
            catch (Exception ex) 
            {
                // Log any exceptions
                Logger.LogEvent(ex.Message, LoggerType.Error);
            }
        }
    }
}
```
## Examples

Below is an example of how you can use the library to log events and errors:

```csharp
// Create an instance of the Logger class
Logger logger = new Logger();

// Log an error event
logger.LogEvent("An error occurred in your application.", LoggerType.Error);

// Log a warning event
logger.LogEvent("A warning message for your application.", LoggerType.Warning);

// Log a successful operation event
logger.LogEvent("The operation was completed successfully.", LoggerType.Success);

// Log a workaround event
logger.LogEvent("A workaround has been applied.", LoggerType.Workaround);
```

## Contributing

Contributions to this project are welcome. If you find any issues or want to suggest improvements, please feel free to create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

