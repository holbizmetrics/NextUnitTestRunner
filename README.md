# NextUnit Testing Framework

## Description

NextUnit is a state-of-the-art testing framework for .NET applications, offering a range of advanced features designed to streamline and enhance the testing process. With its user-friendly design and robust functionality, NextUnit provides a powerful tool for developers to ensure the quality and reliability of their software.

## Key Features

- **Event-Driven Execution**: Utilize event handlers for before and after test runs, as well as during test execution, enabling fine-grained control and customized workflows.
  
- **Custom Test Discovery**: Leverage the flexible test discovery mechanism that supports dynamic identification of test methods, enhancing test suite organization and management.

- **Threaded Execution**: Execute tests concurrently in separate threads, promoting faster test runs and efficient utilization of system resources.

- **Attribute-Based Test Control**: Implement a wide range of test attributes to control test execution, including conditional execution, retry logic, and data injection.

- **Dependency Injection Support**: Seamlessly integrate dependency injection within tests, allowing for cleaner code and improved test maintainability.

- **Hardware Snapshot Capabilities**: Capture hardware snapshots to understand the environment in which tests are run, useful for debugging and performance analysis.

- **Extensive Customization**: Customize test runs with a variety of attributes, catering to complex testing scenarios and specific requirements.

- **Fuzzing Support**: Utilize fuzzing attributes to automatically test methods with a range of inputs, enhancing test coverage and robustness.

- **Integrated AutoFixture Support**: Automatically generate test data using AutoFixture integration, simplifying the setup of complex test scenarios.

- **Advanced Permutation Strategies**: Experiment with different permutation strategies like pairwise or orthogonal array testing for parameterized tests.

- **Seamless Integration with Visual Studio**: Enjoy seamless integration with Visual Studio, including support for test grouping and discovery in the Test Explorer.

Test Run Output Example (in console using the TestRunnerProject):

![image](https://github.com/holbizmetrics/NextUnitTestRunner/assets/48716952/ce8fb2f7-aa22-4b26-ac04-8dc2d2531034)

Test Run Output Example (in Test Explorer, Visual Studio):

![image](https://github.com/holbizmetrics/NextUnitTestRunner/assets/48716952/9948619b-5520-4574-abcf-0f8adcba97fb)

Now it's finally possible to use it in your own class libraries as intended, if the correct packages/references are added:

![image](https://github.com/holbizmetrics/NextUnitTestRunner/assets/48716952/4d563bd7-4734-4b96-88a4-2752ef58d0a1)

## Installation

NextUnit can be installed via NuGet Package Manager. To install, run the following command in your package manager console:

```bash
Install-Package NextUnit
