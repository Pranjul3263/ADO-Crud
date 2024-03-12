namespace AdoApplication.Models
{
    public class Query
    {


        /*
              proc function

                CREATE PROC USP_GET_COUNTRY
        AS
        BEGIN
        SELECT ID ,NAME FROM COUNTRY(NOLOCK)
        END
        ----------------------------------

        CREATE PROC USP_GET_STATE
        @ID INT
        AS
        BEGIN
        SELECT ID ,NAME FROM STATE(NOLOCK) WHERE country_id=@ID
        END
        GO

        CREATE PROC USP_GET_CITY
        @ID int
        AS
        BEGIN
        SELECT ID ,NAME FROM CITY(NOLOCK) WHERE state_id=@Id
        END
        GO

        EXEC USP_GET_COUNTRY;
        EXEC USP_GET_STATE 1;
        EXEC USP_GET_CITY 2;



         */


        /*             
                         create database AdoDemoDatabase
use AdoDemoDatabase

create table country(
id int primary key identity,
name varchar(50));

create table state(
id int primary key identity,
name varchar(50),
country_id int foreign key references country(id));

create table city(
id int primary key identity,
name varchar(50),
state_id int foreign key references state(id));

create table customer(
id int primary key identity,
name varchar(50),
email varchar(100),
mobile varchar(20),
gender varchar(10),
country_id int foreign key references country(id),
state_id int foreign key references state(id),
city_id int foreign key references city(id));

create table profile_gallery(
id int primary key identity,
path varchar(500),
isActive tinyint,
customer_id int foreign key references customer(id),
)

select * from country;
select * from state;
select * from city;
select * from customer;
select * from profile_gallery;

insert into country values
('INDIA'),
('USA'),
('CHINA'),
('NEPAL');

insert into state values
('DELHI',1),
('UTTARPRADESH',1),
('BIHAR',1),
('WASHINGTON',2),
('CALIFORNIA',2),
('SANGHAI',3),
('BIZING',3)

insert into city values
('SOUTH EX',1),
('BARANASI',2),
('LUCKNOW',2),
('MEERUT',2),
('PATNA',3);




        create proc usp_save_customer(
@name varchar(50),
@email varchar(60),
@mobile varchar(70),
@gender varchar(20),
@country_id int,
@state_id int,
@city_id int
)
as 
begin
if(Not exists (select 1 from customer where email=@email))
begin
      insert into customer(
	  name,email,mobile,gender,country_id,state_id,city_id)
	  values(@name,@email,@mobile,@gender,@country_id,@state_id,@city_id)
	  select 1 AS result 
end
else
begin
select 2 as result
end
end



        create proc usp_get_customer
as begin

select cust.id,cust.name,cust.email,cust.mobile,cust.gender
 , cnty.name country, stt.name state,cty.name city from customer(nolock) cust
 left join
 country(nolock) cnty on cust.country_id=cnty.id
 left join
 state(nolock) stt on cust.state_id=stt.id
 left join
 city(nolock) cty on cust.city_id=cty.id
 end

        //update and delete query
        exec usp_get_customer

Alter table customer add isActive tinyint


create proc usp_delete_customer
@id int
as
begin
update customer set isActive=0 where id=@id
select 1 as result
end


sp_helptext usp_save_customer


alter proc usp_save_customer(  
@name varchar(50),  
@email varchar(60),  
@mobile varchar(70),  
@gender varchar(20),  
@country_id int,  
@state_id int,  
@city_id int  
)  
as   
begin  
if(Not exists (select 1 from customer where email=@email))  
begin  
      insert into customer(  
   name,email,mobile,gender,country_id,state_id,city_id,isActive)  
   values(@name,@email,@mobile,@gender,@country_id,@state_id,@city_id,1)  
   select 1 AS result   
end  
else  
begin  
select 2 as result  
end  
end


sp_helptext usp_get_customer

alter proc usp_get_customer  
as begin  
  
select cust.id,cust.name,cust.email,cust.mobile,cust.gender  
 , cnty.name country, stt.name state,cty.name city from customer(nolock) cust  
 left join  
 country(nolock) cnty on cust.country_id=cnty.id  
 left join  
 state(nolock) stt on cust.state_id=stt.id  
 left join  
 city(nolock) cty on cust.city_id=cty.id  
 where cust.isActive=1
 end


 update customer set  isActive=1;

 exec usp_get_customer

 exec usp_delete_customer 2




        update and delete


        select * from customer
sp_helptext usp_get_customer
sp_helptext usp_save_customer

alter procedure usp_update_customer1
@id int,
@name varchar(50),
@email varchar(60),
@mobile varchar(70),
@gender varchar(20),
@country_id int,    
@state_id int,    
@city_id int  

as
begin 
declare @rowcount int=0
begin try
set @rowcount=(select count(1) from customer with(nolock) where id=@id)

if(@rowcount>0)
begin
begin tran
update customer set 
name=@name,
email=@email,
mobile=@mobile,
gender=@gender,
country_id=@country_id,
state_id=@state_id,
city_id=@city_id
where id=@id
commit tran
end
end try
begin catch
rollback tran
end catch
end


exec usp_update_customer1 1,'upebdra222 rajput','rajraj@gmail.com','23424672477','male',1,2,1;

*/


    }
}
