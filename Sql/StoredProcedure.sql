Create or alter trigger add_new_customers
On customers
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
	Set @UpdateBy = (Select @UpdateBy from inserted);

	Insert into Customers (CustomerCode, PhoneNumber, CustomerName, IsBanned, BannedReason, BranchCode, UpdateBy) values
	(@CustomerCode, @PhoneNumber, @CustomerName, @IsBanned, @BannedReason, @BranchCode, @UpdateBy);
End
Go