# DotCoverCoverageValidator
Package for check test coverage threshold!

## Background
When using [dotCover](https://www.jetbrains.com/dotcover/) a report file is generated. That report will tell what percentage of your code that is covered by tests. This tool can be used to validate if your tests has a code coverage over the threshold you set.

https://www.jetbrains.com/dotcover/

## Prerequisites
You need to have a generated report of type json.

## Usage
`dotcovercoveragecheck --threshold 80`

`dotcovercoveragecheck --threshold 80 --dotcoveroutputpath .\coverageOutput\dotCover.Output.json`

## Arguments
| Argument           | Description                  | Default value        |
| ------------------ | ---------------------------- | -------------------- |
| threshold          | Percentage as a number       | *Required*           |
| dotcoveroutputpath | Path to dotCover output file | dotCover.Output.json |

## Output
This tool will return a ´0´ if the threshold is met. It will return a `1` if there is an error or if the threshold is not met.