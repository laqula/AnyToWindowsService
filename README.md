# AnyToWindowsService
Windows service that runs any external program or command.

Simple Windows service based on Topshelf. It runs any valid windows command in specified periods of time.

## Requirements
.net 5.0 runtime

## Usage
1. publish code to specified directory
1. edit config file
1. install windows service
1. run

## Settings
Setting are places in two JSON files: appsettings.json and appsettings.serilog.json. Serilog configuration is described on Serilog project site.

### Application settings
| Name | Description | Notes |
| ------------ | ------------ | ------------ |
| serviceName | Service name which will be used during installation in Windows. | |
| serviceDescription | Service description which will be used during installation in Windows. | It can be `null` |
| displayName | Service display name which will be used during installation in Windows. | If it is `null` then serviceName will be used. |
| command | Command to execute in specified intervals. | Any valid windows cmd command. |
| intervalInSec | Interval in seconds between command executions. | Interval is counted between starts (not from end to start). Default value `0`. |
| runOnlyOnce | If `true` command will be run only once. | It can be used for command which have infinite loop inside. |
| maxWaitForFinishInSec | Time in seconds after which command will be killed. | It will be used for long running commands which can't handle serivce stop command. |

### Serilog settings
By default only errors will be logged inside "log" subdirectory.  
more info: https://serilog.net/

## Installation
`AnyToWindowsService install` (in command prompt, runned as administrator)  
more info: https://topshelf.readthedocs.io/en/latest/overview/commandline.html

## Command examples
If you want to use double quotes in your command, you must escape them with \. Backslashes must by escaped with another backslash \\ or must be converted into /

environment variable, file output, escaped backslashes
`echo %USERNAME% >> c:\\tmp\\AnyToWindowsService.txt`

escaped double quotes, slashes instead of backslashes 
`\"C:/python-3.9.8/python.exe\" \"C:/python/selenium/tmp.py\"`
