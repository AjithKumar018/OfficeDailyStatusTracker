# Office Status Tracker

## Connect DB and Models Project
1. First, Install the following Packages from nuget
	* Microsoft.EntityFrameworkCore
	* Microsoft.EntityFrameworkCore.Tools
	* Microsoft.EntityFrameworkCore.SqlServer
2. Run the following Code to Create Table Models
	Scaffold-DbContext "Server=<SERVER_NAME>;Database=<DB_NAME>;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Tables -force
