SET IDENTITY_INSERT [Accounts].[Account] ON
INSERT INTO [Accounts].[Account] ([Id],
                                 [TypeId],
                                 [EntityTypeId],
								 [UserCode],
								 [Username],
								 [PasswordHash],
								 [CreationDateTime],
								 [EmailVerificationStatusId],
								 [MobileVerificationStatusId],
								 [StateId],
								 [Roles],
								 [VerificationStatusId]) 
						  VALUES(1,
						         1,
                                 1,
								 '123456',
								 'admin',
								 '1000:2b7/4PNTOGtb2fuUxLADzccSl3bfXxwg:KTS5kYwZzsePZswA0dDj9tg1QBS6SxOR', --123
								 GETDATE(),
								 '1',
								 '1',
								 1,
								 N'admin',
								 1)
SET IDENTITY_INSERT [Accounts].[Account] OFF

