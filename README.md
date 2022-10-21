### Lamps Plus - Multithreading Timeout
- The issue here is that the tests only complete when double the typical threads are called for. Lamps Plus doesn't have the capacity to allow for this on their grid. 

To Reproduce: 

0. *Windows machine reccommended, haven\'t tried this on Mac*
1. Install the needed plugins (Eyes, XUnit Priority, SkippableFact). Namely, you'll also need xunit runner console. Also fill in Applitools API Key and SauceLabs Credentials. 
2. Once you can build and run tests, go to the bin/Debug directory in console. 
3. Once there, execute the following (replacing your path to the xunit console): 
`"C:\Users\Casey\.nuget\packages\xunit.runner.console\2.4.2\tools\net472\xunit.console.exe" net47/lampsPlus_env_oct.dll -maxthreads 13`

4. Note that this test will hang. When you use -maxthreads 26, this will work. Lamps Plus found that the threads need to be double the number of tests. **This was not the case pre-USDK** and it\'s not something that their grid can support. 

#### Notes: 
- Portions of the code for testing in different ways are commented out. You shouldn\'t need to uncomment anything to replicate the issue. 
- I was able to replicate this in Sauce Labs on a simulated iPhone (uncommented), but was not able to replicate on a local Chromedriver as the customer did. 