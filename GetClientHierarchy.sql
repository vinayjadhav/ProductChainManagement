Alter Proc GetClientHierarchy
(
	@ClientId Int
)
AS
Begin
	Declare @count Int
	Declare @ClientChain Table
	(
		ClientId Int,
		Name Varchar(200),
		ParentId Int,
		cnt Int
	)

	Declare @Temp Table
	(
		ClientId Int
	)
	Insert Into @ClientChain Select Client_Id,Name,0,0 From ClientMaster Where Client_Id = @ClientId
	Set @count = 1
	Insert Into @ClientChain Select Client_Id,Name,Ref_Client_Id,@count From ClientMaster Where Ref_Client_Id = @ClientId
	Insert Into @Temp Select Client_Id From ClientMaster Where Ref_Client_Id = @ClientId

	While(1=1)
	Begin
		If Exists(Select 1 From ClientMaster Where Ref_Client_Id In (Select ClientId From @Temp))
		Begin
			Set @count = @count + 1
			Insert Into @ClientChain Select Client_Id,Name,Ref_Client_Id,@count From ClientMaster Where Ref_Client_Id In (Select ClientId From @Temp)
			Delete From @Temp
			Insert Into @Temp Select Client_Id From ClientMaster Where Client_Id In (Select ClientId From @ClientChain Where cnt = @count)
		End
		Else
		Begin
			BREAK
		End
	End

	Select ClientId AS ClientID,
	       Name AS Name,
		   ParentId AS ParentId 
      From @ClientChain
End