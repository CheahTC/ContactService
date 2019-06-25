# ContactService
Project to be run locally: 
1. Install the Entity Framework from Package Manager Console in Visual Studios IDE.
2. Install knockout JS packages from Package Manager Console in Visual Studios IDE.
3. To set up a new LocalDB, back up the Configuration.cs file before removing the files in the "Migration" folder. 
4. Then run the commands below using the Package Manager Console:
	a. Enable-Migration
	b. Add-Migration Initial
5. Copy and paste the code in the Configuration.cs backup to the new Configuration.cs file.
6. Run command "Update-Database" using Package Manager Console. This should provide some sample data.

Assumptions:
- There is no need to group up contact numbers by person. 
- Addition of additional phone numbers is seldom done.
- If numbers were not initially activated, the details section will give allow user to activate the number.
- No number deactivation is required.
- New numbers need to be added first before it can be activated.


Notes:
- Not able to activate phone numbers, as it requires implementing the PATCH request which I am not familiar with. 
This is due to the PUT request causing a foreign key violation, since PUT requires the all the data in the model to be present.
- I had tried a workaround, namely deleting the existing entry to update the activation flag, then posting a new record, but it was unsuccessful.


