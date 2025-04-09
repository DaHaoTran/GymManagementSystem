Create or alter trigger add_new_customers
On Customers
Instead of insert
As
Begin
	Declare @CustomerCode Varchar(12), @PhoneNumber Char(10), @CustomerName Nvarchar(max),
		@IsBanned bit, @BannedReason Nvarchar(max), @BranchCode Varchar(6), @UpdateBy Varchar(10);
	
	Declare @CodeSetString Varchar(10);
	Set @CodeSetString = '0000000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Customers) + 1;

	While exists (Select CustomerCode from Customers where CustomerCode = (Select CustomerCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @CustomerCode = (Select CustomerCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PhoneNumber = (Select PhoneNumber from inserted);
	Set @CustomerName = (Select CustomerName from inserted);
	Set @IsBanned = 0;
	Set @BannedReason = null;
	Set @BranchCode = (Select BranchCode from inserted);
	Set @UpdateBy = (Select UpdateBy from inserted);

	Insert into Customers (CustomerCode, PhoneNumber, CustomerName, IsBanned, BannedReason, BranchCode, UpdateBy) values
	(@CustomerCode, @PhoneNumber, @CustomerName, @IsBanned, @BannedReason, @BranchCode, @UpdateBy);
End
Go

Create or alter trigger add_new_accounts
On Accounts
Instead of insert
As
Begin
	Declare @AccountCode Varchar(10), @PhoneNumber Char(10), @IdNumber Char(12), @FullName Nvarchar(max), @Age Int,
		@LivingAt Nvarchar(max), @Password Nvarchar(max), @UpdateBy Varchar(10), @RoleId Int, @IsDeleted Bit, @SalaryCode Varchar(5);
	
	Declare @CodeSetString Varchar(8);
	Set @CodeSetString = '00000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Accounts Where AccountCode Like (Select AccountCode from inserted) COLLATE SQL_Latin1_General_CP1_CI_AS) + 1;

	While exists (Select AccountCode from Accounts where AccountCode = (Select AccountCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @AccountCode = (Select AccountCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PhoneNumber = (Select PhoneNumber from inserted);
	Set @IdNumber = (Select IdNumber from inserted);
	Set @FullName = (Select FullName from inserted);
	Set @Age = (Select Age from inserted);
	Set @LivingAt = (Select LivingAt from inserted);
	Set @Password = (Select Password from inserted);
	Set @UpdateBy = (Select UpdateBy from inserted);
	Set @RoleId = (Select RoleId from inserted);
	Set @IsDeleted = 0;
	Set @SalaryCode = (Select SalaryCode from inserted);

	Insert into Accounts (AccountCode, PhoneNumber, IdNumber, FullName, Age, LivingAt, Password, UpdateBy, RoleId, IsDeleted, SalaryCode) Values
		(@AccountCode, @PhoneNumber, @IdNumber, @FullName, @Age, @LivingAt, @Password, @UpdateBy, @RoleId, @IsDeleted, @SalaryCode);
End
Go

Create or alter trigger add_new_branches
On Branches
Instead of insert
As
Begin
	Declare @BranchCode Varchar(6), @Address Nvarchar(450), @BranchName Nvarchar(max), @QuantityOfStaffs Int, @QuantityOfPTs Int,
		@QuantityOfWorkers Int, @AdminUpdate Varchar(10), @IsDeleted Bit;
	
	Declare @CodeSetString Varchar(4);
	Set @CodeSetString = '0000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Branches) + 1;

	While exists (Select BranchCode from Branches where BranchCode = (Select BranchCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @BranchCode = (Select BranchCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @Address = (Select Address from inserted);
	Set @BranchName = (Select BranchName from inserted);
	Set @QuantityOfPTs = 0;
	Set @QuantityOfStaffs = 0;
	Set @QuantityOfWorkers = 0;
	Set @AdminUpdate = (Select AdminUpdate from inserted);
	Set @IsDeleted = 0;

	Insert into Branches (BranchCode, Address, BranchName, QuantityOfStaffs, QuantityOfPTs, QuantityOfWorkers, AdminUpdate, IsDeleted) Values
		(@BranchCode, @Address, @BranchName, @QuantityOfStaffs, @QuantityOfPTs, @QuantityOfWorkers, @AdminUpdate, @IsDeleted);
End
Go

Create or alter trigger add_new_equipments
On Equipment
Instead of insert
As
Begin
	Declare @EquipCode Varchar(10), @BranchCode Varchar(6), @EquipName Nvarchar(max), @Status Nvarchar(max), @Note Nvarchar(max),
		@StaffUpdate Varchar(10), @AdminUpdate Varchar(10), @IsReceived Bit, @IsDeleted Bit;
	
	Declare @CodeSetString Varchar(8);
	Set @CodeSetString = '00000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Equipment) + 1;

	While exists (Select EquipCode from Equipment where EquipCode = (Select EquipCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @EquipCode = (Select EquipCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @BranchCode = (Select BranchCode from inserted);
	Set @EquipName = (Select EquipName from inserted);
	Set @Status = (Select Status from inserted);
	Set @Note = (Select Note from inserted);
	Set @StaffUpdate = (Select StaffUpdate from inserted);
	Set @AdminUpdate = (Select AdminUpdate from inserted);
	Set @IsReceived = (Select IsReceived from inserted);
	Set @IsDeleted = 0;

	Insert into Equipment (EquipCode, BranchCode, EquipName, Status, Note, StaffUpdate, AdminUpdate, IsReceived, IsDeleted) Values
		(@EquipCode, @BranchCode, @EquipName, @Status, @Note, @StaffUpdate, @AdminUpdate, @IsReceived, @IsDeleted);
End
Go

Create or alter trigger add_new_salaries
On Salaries
Instead of insert
As
Begin
	Declare @SalaryCode Varchar(5), @SalaryType Nvarchar(450), @PricesApply Money, @UpdateDate Datetime2(7);
	
	Declare @CodeSetString Varchar(3);
	Set @CodeSetString = '000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Salaries) + 1;

	While exists (Select SalaryCode from Salaries where SalaryCode = (Select SalaryCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @SalaryCode = (Select SalaryCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @SalaryType = (Select SalaryType from inserted);
	Set @PricesApply = (Select PricesApply from inserted);
	Set @UpdateDate = (Select UpdateDate from inserted);

	Insert into Salaries (SalaryCode, SalaryType, PricesApply, UpdateDate) Values
		(@SalaryCode, @SalaryType, @PricesApply, @UpdateDate);
End
Go

Create or alter trigger add_new_service_packages
On ServicePackages
Instead of insert
As
Begin
	Declare @PackageCode Varchar(5), @PackageName Nvarchar(450), @Price Money, @MemberQuantity Int, 
		@NumberOfDays Int, @IsDeleted Bit, @AdminUpdate Varchar(10);
	
	Declare @CodeSetString Varchar(3);
	Set @CodeSetString = '000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from ServicePackages) + 1;

	While exists (Select PackageCode from ServicePackages where PackageCode = (Select PackageCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @PackageCode = (Select PackageCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PackageName = (Select PackageName from inserted);
	Set @Price = (Select Price from inserted);
	Set @MemberQuantity = (Select MemberQuantity from inserted);
	Set @NumberOfDays = (Select NumberOfdays from inserted);
	Set @IsDeleted = 0;
	Set @AdminUpdate = (Select AdminUpdate from inserted);

	Insert into ServicePackages (PackageCode, PackageName, Price, MemberQuantity, NumberOfDays, IsDeleted, AdminUpdate) Values
		(@PackageCode, @PackageName, @Price, @MemberQuantity, @NumberOfDays, @IsDeleted, @AdminUpdate);
End
Go
