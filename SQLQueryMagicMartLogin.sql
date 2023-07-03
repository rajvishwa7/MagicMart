create table tbl_MagicLogin
(
	LoginId int primary key identity(1,1),
	LoginName varchar(50),
	LoginPwd varchar(50)
)

select * from tbl_MagicLogin

insert into tbl_MagicLogin values('admin','admin')

create procedure sp_login
(
	@Lname varchar(50),
	@Lpwd varchar(50)
)
as
begin
	select * from tbl_MagicLogin
	where LoginName = @Lname and LoginPwd = @Lpwd
end

exec sp_login 'admin','admin'
