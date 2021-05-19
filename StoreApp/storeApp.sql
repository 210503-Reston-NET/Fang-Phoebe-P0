--drop database StoreAppDB
--create database StoreAppDB

--drop table locations
--drop table customers
--drop table orders
--drop table orderItems
--drop table products
--drop table inventories

-- table defintion
create table locations
(
	id int identity primary key,
	Name nvarchar(50) not null,
	Address nvarchar(50) not null,
)

create table customers
(
	id int identity primary key,
	LastName nvarchar(50) not null,
	MiddleName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	PhoneNumber nvarchar(50) not null,
	--Address nvarchar(50) not null,
)

create table products
(
	id int identity primary key,
	name nvarchar(50) not null,
	price decimal(6,2) not null,
    barcode nvarchar(50) not null,
)

create table inventories(
    id int identity primary key,
    quantity int not null,
    locationId int references locations(id) on delete cascade,
    productId int references products(id) on delete cascade
)

create table orders
(
	id int identity primary key,
	orderDate date not null,
	total decimal(6,2) not null,
	locationId int references locations(id) on delete cascade,
    customerId int references customers(id) on delete cascade
)

create table orderItems
(
	id int identity primary key,
	quantity int not null,
	productId int references products(id) on delete cascade,
	orderId int references orders(id) on delete cascade
)


--seeding data
insert into locations (Name, Address) values
('NY-Flushing', '133-35 Roosevelt Ave Flushing NY 11354'),
('OR-Bridgeport', '7319 SW Bridgeport Rd.Tigard OR 97224'),
('WA-Bellevue', '1031 Bullevue Square Bellevue WA 98004'),
('WA-Seattle', '2245 8th Ave., R1-1A Seattle WA 98121'),
('CA-Fremont', '46873 Warm Spring Blvd Fremont CA 94539'),
('CA-San Francisco', '1320 4th Street San Francisco CA 94158')

insert into customers (LastName, MiddleName, FirstName, PhoneNumber) VALUES
('Fang', '', 'Phoebe', '2063317069'),
('Smith', 'M', 'Jonh', '2067798888')

insert into products(name, price, barcode) values
('Classic Milk Tea', 3.99, 'c-m-t-01'),
('Boba Milk Tea', 4.99, 'b-m-t-01')

select * from locations
select * from customers
select * from products
select * from inventories
select * from orders
select * from orderItems
select * from products

--inner join orders, customer, location, orderitems, products
select firstname, middlename, lastname, phoneNumber, l.name, o.id, orderDate, total, p.name as ProductName
from orders o
inner join customers c on o.customerId = c.Id
inner join locations l on o.locationId = l.Id
inner join orderItems oi on oi.orderId = o.Id
inner join products p on oi.productId = p.Id

-- inner join orderitems with products
select orderId, p.name, p.price, p.barcode, oi.quantity
from orderItems oi 
inner join products p on p.id = oi.productId


