create database MagicMart
use MagicMart

create table tbl_MagicUser
(
	MagicId int primary key identity(1,1),
	UserName varchar(50), 
	UserEmail varchar(50), 
	UserMobile varchar(50),
	UserAge varchar(50),
	UserId varchar(50),
	UserGender varchar(50),
	UserAddress varchar(50),
	UserCity varchar(50),
	UserState varchar(50),
)

select * from tbl_MagicUser

create procedure sp_insert
(
	@uName varchar(50), 
	@uEmail varchar(50), 
	@uMobile varchar(50),
	@uAge varchar(50),
	@uId varchar(50),
	@uGender varchar(50),
	@uAddress varchar(50),
	@uCity varchar(50),
	@uState varchar(50)
)
as
begin
insert into tbl_MagicUser (UserName,UserEmail,UserMobile,UserAge,UserId,UserGender,UserAddress,UserCity,UserState)
	values(@uName,@uEmail,@uMobile,@uAge,@uId,@uGender,@uAddress,@uCity,@uState)
end

----truncate table tbl_Magicuser----

create procedure sp_fetch
as
begin
	select * from tbl_MagicUser
end

create procedure sp_update
(
	@uName varchar(50), 
	@uEmail varchar(50), 
	@uMobile varchar(50),
	@uAge varchar(50),
	@uId varchar(50),
	@uGender varchar(50),
	@uAddress varchar(50),
	@uCity varchar(50),
	@uState varchar(50),
	@ID int
)
as
begin
	update tbl_MagicUser set 
	UserName=@uName,UserEmail=@uEmail,UserMobile=@uMobile,UserAge=@uAge,UserId=@uId,
	UserGender=@uGender,UserAddress=@uAddress,UserCity=@uCity,UserState=@uState
	where MagicId = @ID
end

create procedure sp_delete
(
@ID int
)
as
begin
	delete tbl_MagicUser where MagicId = @ID
end

create procedure sp_search
(
	@searchData varchar(100)
)
as 
begin
	select * from tbl_MagicUser where UserName like '%'+ @searchData + '%'
	--- select * from tbl_MagicUser where MagicId like '%'+ @searchData + '%' --- --if you want to Search with ID number--
end



