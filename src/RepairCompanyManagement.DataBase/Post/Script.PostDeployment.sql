--- Roles
insert into dbo.AspNetRoles(Id, Name)
values ('15269863-61E7-4877-BE2A-A22602CE742D', 'Manager'),
('15269863-61E7-4877-BE2A-A22602CE742E', 'Customer'),
('15269863-61E7-4877-BE2A-A22602CE742F', 'Admin'),
('15269863-61E7-4877-BE2A-A22602CE742G', 'Employee')

--- User
insert into dbo.[AspNetUsers](Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Surname, FirstName, LastName, Balance)
values ('88527CB8-A870-4757-AED9-DB0BC51624E2', 'manager@gmail.com', 0, 'AJCcr9bpMiB47T4+SMVYdlNTkZeztIazjCxNgKxZwweI+OkJ4Vzembs/YRmPjedcUg==', '9c392224-18f2-4540-9e1d-9a44afa96e88', NULL, 0, 0, NULL, 0, 0, 'manager@gmail.com', 'managerov', 'manager', 'managerovich' , 0),
('88527CB8-A870-4757-AED9-DB0BC51624E3', 'customer@gmail.com', 0, 'AJCcr9bpMiB47T4+SMVYdlNTkZeztIazjCxNgKxZwweI+OkJ4Vzembs/YRmPjedcUg==', '9e392224-18f2-4540-9e1d-9a44afa96e87', NULL, 0, 0, NULL, 0, 0, 'customer@gmail.com', 'userov', 'user', 'userovich', 0),
('88527CB8-A870-4757-AED9-DB0BC51624E4', 'admin@gmail.com', 0, 'AJCcr9bpMiB47T4+SMVYdlNTkZeztIazjCxNgKxZwweI+OkJ4Vzembs/YRmPjedcUg==', '9b392224-18f2-4540-9e1d-9a44afa96e86', NULL, 0, 0, NULL, 0, 0, 'admin@gmail.com', 'adminov', 'admin', 'adminovich', 0),
('88527CB8-A870-4757-AED9-DB0BC51624E5', 'employee@gmail.com', 0, 'AJCcr9bpMiB47T4+SMVYdlNTkZeztIazjCxNgKxZwweI+OkJ4Vzembs/YRmPjedcUg==', '9b392224-18f2-4540-9e1d-9a44afa96e85', NULL, 0, 0, NULL, 0, 0, 'employee@gmail.com', 'employeev', 'employee', 'employeevich', 0)

--- UserRoles
insert into dbo.[AspNetUserRoles](UserId, RoleId)
values('88527CB8-A870-4757-AED9-DB0BC51624E2', '15269863-61E7-4877-BE2A-A22602CE742D'),
('88527CB8-A870-4757-AED9-DB0BC51624E3', '15269863-61E7-4877-BE2A-A22602CE742E'),
('88527CB8-A870-4757-AED9-DB0BC51624E4', '15269863-61E7-4877-BE2A-A22602CE742F'),
('88527CB8-A870-4757-AED9-DB0BC51624E5', '15269863-61E7-4877-BE2A-A22602CE742G')

insert into dbo.[Customer](Gender, IdentityUserID)
values('ћужской', '88527CB8-A870-4757-AED9-DB0BC51624E3')

insert into dbo.[Specialization](Name, Description)
values('Ёлектрик', 'Ёлектричит')

insert into dbo.Brigade(Title, IdSpecialization)
values('’ром', 1)

insert into dbo.JobPosition(Title, Purpose)
values('Ѕригадир', 'Ѕригадирит')

insert into dbo.Employee(IdBrigade, Salary, IdJobPosition, IdentityUserID)
values(1, 1000, 1, '88527CB8-A870-4757-AED9-DB0BC51624E5')

insert into dbo.Manager([Address], Salary, IdentityUserID, DateOfBirth)
values('ѕечальна€ область, тоскливый район, город грусть, проспект разочарование, дом 13', 1000, '88527CB8-A870-4757-AED9-DB0BC51624E2', '2000-12-12')