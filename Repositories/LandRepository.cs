using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Modal;
using static ViltrapportenApi.Modal.ApiRoutes;

namespace ViltrapportenApi.Repositories
{
    public class LandRepository : ILandRepository//IDisposable
    {
        private readonly ViltrapportenSystemContext _context;
        public LandRepository(ViltrapportenSystemContext context)
        {
            _context = context;
        }

        public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetLandAndOwnersAsync(int unitId)
        {
            var lands = new List<LandMapped>();
            var landOwners = new List<LandOwnerMapped>();
            var connectionString = _context.Database.GetConnectionString(); // var connection = _context.Database.GetDbConnection() is now work for subsequent report
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetLandDetailsUnderUnit_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@unitId", unitId));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Read Land data
                            while (await reader.ReadAsync())
                            {

                                var land = new LandMapped
                                {
                                    LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                    Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
                                    MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
                                    SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
                                    PlotNo = reader.GetString(reader.GetOrdinal("PlotNo")),
                                    AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
                                    AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
                                    AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
                                    TotalArea = reader.GetFloat(reader.GetOrdinal("TotalArea")),
                                    Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                    OwnershipTypeId = reader.GetInt32(reader.GetOrdinal("OwnershipTypeId")),
                                    OwnershipType = reader.GetString(reader.GetOrdinal("OwnershipType")),
                                    ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
                                    LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    UnitId = reader.GetInt32(reader.GetOrdinal("UnitID")),
                                    Unit = reader.GetString(reader.GetOrdinal("Unit")),
                                    LandTypeId = reader.GetInt32(reader.GetOrdinal("LandTypeId")),
                                    ParentUId = reader.GetInt32(reader.GetOrdinal("ParentUId")),
                                    ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
                                };
                                lands.Add(land);
                            }

                            await reader.NextResultAsync();

                            while (await reader.ReadAsync())
                            {
                                var landOwner = new LandOwnerMapped
                                {
                                    LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
                                    AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                    AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                    AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                    BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                    Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                    LandId = reader.GetInt32(reader.GetOrdinal("LandId"))
                                };
                                landOwners.Add(landOwner);
                            }
                        }
                    }
                }
                catch
                {
                    // Optionally, handle any errors here
                    throw;
                }

                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }

            return (lands, landOwners);
        }

        public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetOwnersLandDetailsUnderUnitAsync(int landOwnerSysUid, int unitId, bool isDnnId)
        {
            var landMappedList = new List<LandMapped>();
            var landOwnerMappedList = new List<LandOwnerMapped>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();


                    using (var command = connection.CreateCommand())
                    {
                        {
                            int isPassedDnnId = isDnnId ? 1 : 0;
                            command.CommandText = "[dbo].[GetOwnersLandDetailsUnderUnit_SP]";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@landOwnerSysUid", landOwnerSysUid));
                            command.Parameters.Add(new SqlParameter("@unitId", unitId));
                            command.Parameters.Add(new SqlParameter("@isPassedDnnId", isPassedDnnId));

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                // Reading first result set
                                while (await reader.ReadAsync())
                                {
                                    var landMapped = new LandMapped
                                    {
                                        LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                        Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
                                        MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
                                        SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
                                        PlotNo = reader.GetString(reader.GetOrdinal("PlotNo")),
                                        AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
                                        AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
                                        AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
                                        TotalArea = reader.GetFloat(reader.GetOrdinal("TotalArea")),
                                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                        OwnershipTypeId = reader.GetInt32(reader.GetOrdinal("OwnershipTypeId")),
                                        OwnershipType = reader.GetString(reader.GetOrdinal("OwnershipType")),
                                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                        DnnUserId = reader.GetInt32(reader.GetOrdinal("DnnUserID")),
                                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                        UnitId = reader.GetInt32(reader.GetOrdinal("UnitID")),
                                        Unit = reader.GetString(reader.GetOrdinal("Unit")),
                                        LandTypeId = reader.GetInt32(reader.GetOrdinal("LandTypeId")),
                                        ParentUId = reader.GetInt32(reader.GetOrdinal("ParentUId")),
                                        ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
                                    };

                                    landMappedList.Add(landMapped);
                                }

                                // Move to the next result set
                                if (await reader.NextResultAsync())
                                {
                                    while (await reader.ReadAsync())
                                    {
                                        var landOwnerMapped = new LandOwnerMapped
                                        {
                                            LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                            SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                            Email = reader.GetString(reader.GetOrdinal("Email")),
                                            ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                            ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
                                            AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                            AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                            AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                            BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                            LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                        };

                                        landOwnerMappedList.Add(landOwnerMapped);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and handle exceptions as necessary
                throw new ApplicationException("Error retrieving owners data.", ex);
            }

            return (landMappedList, landOwnerMappedList);
        }

        public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetSharedOwnersLandDetailsUnderUnitAsync(string landIdStr, int unitId)
        {
            var lands = new List<LandMapped>();
            var landOwners = new List<LandOwnerMapped>();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "[dbo].[GetSharedOwnersLandDetailsUnderUnit_SP]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@landIdStr", landIdStr));
                    command.Parameters.Add(new SqlParameter("@unitId", unitId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var land = new LandMapped
                            {
                                LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
                                MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
                                SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
                                PlotNo = reader.GetString(reader.GetOrdinal("PlotNo")),
                                AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
                                AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
                                AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
                                TotalArea = reader.GetFloat(reader.GetOrdinal("TotalArea")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                OwnershipTypeId = reader.GetInt32(reader.GetOrdinal("OwnershipTypeId")),
                                OwnershipType = reader.GetString(reader.GetOrdinal("OwnershipType")),
                                ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
                                LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                UnitId = reader.GetInt32(reader.GetOrdinal("UnitID")),
                                Unit = reader.GetString(reader.GetOrdinal("Unit")),
                                LandTypeId = reader.GetInt32(reader.GetOrdinal("LandTypeId")),
                                ParentUId = reader.GetInt32(reader.GetOrdinal("ParentUId")),
                                ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
                            };
                            lands.Add(land);
                        }

                        await reader.NextResultAsync();

                        while (await reader.ReadAsync())
                        {
                            var landOwner = new LandOwnerMapped
                            {
                                LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
                                AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                LandId = reader.GetInt32(reader.GetOrdinal("LandId"))
                            };
                            landOwners.Add(landOwner);
                        }
                    }
                }
            }

            return (lands, landOwners);
        }

        public async Task<(List<LandOwnerMapped> singleLandOwners, List<LandOwnerMapped> multipleLandOwners, List<LandOwnerMapped> GetLandOwnersDetails)> GetLandOwnersDetailsAsync(int unitId)
        {
            var singleLandOwners = new List<LandOwnerMapped>();
            var multipleLandOwners = new List<LandOwnerMapped>();
            var additionalLandOwners = new List<LandOwnerMapped>();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "[dbo].[GetLandOwnersDetailsUnderUnit_SP]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@unitId", unitId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Read single land owner data from first result set
                        while (await reader.ReadAsync())
                        {
                            var landOwner = new LandOwnerMapped
                            {
                                LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
                                AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
                                AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
                                LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                LandNotes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"))
                            };
                            singleLandOwners.Add(landOwner);
                        }

                        // Move to the next result set
                        await reader.NextResultAsync();

                        // Read multiple land owner data from second result set
                        while (await reader.ReadAsync())
                        {
                            var landOwner = new LandOwnerMapped
                            {
                                LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
                                AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
                                AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
                                LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
                                MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
                                SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
                                LandNotes = reader.IsDBNull(reader.GetOrdinal("LandNote")) ? null : reader.GetString(reader.GetOrdinal("LandNote")),
                                Unit = reader.GetString(reader.GetOrdinal("Unit")),
                                ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
                            };
                            multipleLandOwners.Add(landOwner);
                        }

                        // Move to the next result set
                        await reader.NextResultAsync();

                        // Read additional land owner data from third result set
                        while (await reader.ReadAsync())
                        {
                            var landOwner = new LandOwnerMapped
                            {
                                LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId"))
                            };
                            additionalLandOwners.Add(landOwner);
                        }
                    }
                }
            }

            return (singleLandOwners, multipleLandOwners, additionalLandOwners);
        }


        public async Task<int> ArchiveLandAsync(int landId, int updatedBy)
        {
            int result;
            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[ArchiveLand_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@landId", landId));
                        command.Parameters.Add(new SqlParameter("@updatedBy", updatedBy));

                        result = await command.ExecuteNonQueryAsync();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }

            return result;
        }

        public async Task<int> ManageLandOwnerDetails(OwnerDetail ownerDetails)
        {
            int result;
            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[ManageLandOwnerDetails_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@sysUId", ownerDetails.SystemUserId));
                        if (ownerDetails.LandId.HasValue && ownerDetails.LandId > 0)
                        {
                            command.Parameters.Add(new SqlParameter("@landId", ownerDetails.LandId.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@landId", DBNull.Value));
                        }
                        command.Parameters.Add(new SqlParameter("@line1", ownerDetails.AddressLine1));
                        command.Parameters.Add(new SqlParameter("@line2", ownerDetails.AddressLine2));
                        command.Parameters.Add(new SqlParameter("@city", ownerDetails.AddressCity));
                        command.Parameters.Add(new SqlParameter("@accNo", ownerDetails.BankAccountNo));
                        command.Parameters.Add(new SqlParameter("@notes", ownerDetails.Notes));

                        result = await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }
            return result;
        }

        public async Task ManageLandOwnerAsync(int landId, int ownerId, int createdBy)
        {
            var connection = _context.Database.GetDbConnection(); // Use the same connection as the DbContext
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // Enlist in the current transaction(bcz this method run withing the transaction)
                    command.CommandText = "[dbo].[ManageLandOwner_SP]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@landId", landId));
                    command.Parameters.Add(new SqlParameter("@ownerId", ownerId));
                    command.Parameters.Add(new SqlParameter("@assignedBy", createdBy));

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task ManageLandOwnerAsync(int landId, int ownerId, int createdBy)
        //{
        //    var connectionString = _context.Database.GetConnectionString(); // var connection = _context.Database.GetDbConnection() is now work for subsequent report
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            await connection.OpenAsync();

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "[dbo].[ManageOwner_SP]";
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.Parameters.Add(new SqlParameter("@landId", landId));
        //                command.Parameters.Add(new SqlParameter("@ownerId", ownerId));
        //                command.Parameters.Add(new SqlParameter("@assignedBy", createdBy));

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //}

        public async Task ArchiveLandOwnerAsync(int landId, int ownerId)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[ArchiveLandOwner_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@landId", landId));
                        command.Parameters.Add(new SqlParameter("@userId", ownerId));

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<List<LandOwnerMapped>> GetLandMultipleOwnersContactOwnerDetailsAsync()
        {
            var multipleLandOwners = new List<LandOwnerMapped>();
            var connectionString = _context.Database.GetConnectionString(); // var connection = _context.Database.GetDbConnection() is now work for subsequent report
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetLandMultipleOwnersContactOwnerDetails_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var landOwner = new LandOwnerMapped
                                {
                                    LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                    AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                    AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                    BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                    Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                    LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
                                    ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId"))
                                };
                                multipleLandOwners.Add(landOwner);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw;
                }

                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }
            return multipleLandOwners;
        }

        public async Task<List<OwnersState>> GetLandOwnerDetailByLandIdAsync(int landId)
        {
            var ownersStates = new List<OwnersState>();


            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetLandOwnersByLandId_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@landId", landId));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var landOwner = new OwnersState
                                {
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    IsSharedLandOwner = reader.GetBoolean(reader.GetOrdinal("IsSharedLandOwner"))
                                };
                                ownersStates.Add(landOwner);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }

            return ownersStates;
        }
        public async Task<LandOwnerMapped> GetLandOwnerDetailsBySysUIdAsync(int systemUserId, int landId)
        {
            var landOwners = new List<LandOwnerMapped>();


            var connectionString = _context.Database.GetConnectionString(); // var connection = _context.Database.GetDbConnection() is now work for subsequent repquest
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetLandOwnerDetailsBySysUId_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@sysUId", systemUserId));
                        command.Parameters.Add(new SqlParameter("@landId", landId));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var landOwner = new LandOwnerMapped
                                {
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
                                    AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
                                    AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
                                    BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
                                    Notes = reader.GetString(reader.GetOrdinal("Notes"))
                                };
                                landOwners.Add(landOwner);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return landOwners.FirstOrDefault();
        }

        public async Task<List<LandOwnerInfo>> GetFilteredUserListAsync(string filter, int userDnnId, int isAdmin)
        {
            var filteredOwnerList = new List<LandOwnerInfo>();


            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetFilteredUserList_SP]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@filter", filter));
                        command.Parameters.Add(new SqlParameter("@userdnnId", userDnnId));
                        command.Parameters.Add(new SqlParameter("@isAdmin", isAdmin));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var filteredOwners = new LandOwnerInfo
                                {
                                    DnnUserId = reader.GetInt32(reader.GetOrdinal("DnnUserID")),
                                    SystemUserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber"))

                                };
                                filteredOwnerList.Add(filteredOwners);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                finally
                {
                    //if (connection.State == System.Data.ConnectionState.Open)
                    //{
                    //    await connection.CloseAsync();
                    //}
                }
            }

            return filteredOwnerList;
        }



        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //}

        //public async Task<int> ArchiveLandAsync(int landId, int updatedBy)
        //{
        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "[dbo].[ArchiveLand_SP]";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.Add(new SqlParameter("@landId", landId));
        //            command.Parameters.Add(new SqlParameter("@updatedBy", updatedBy));

        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }

        //    return landId;
        //}


        //public async Task<int> ArchiveLand(int landId, int deletedBy)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@landId", landId, DbType.Int32);
        //        parameters.Add("@updatedBy", deletedBy, DbType.Int32);

        //        // Calling the stored procedure
        //        await connection.ExecuteAsync("[dbo].[Archive]", parameters, commandType: CommandType.StoredProcedure);

        //        // Assuming that there is no need to return anything from the stored procedure
        //        return landId;
        //    }
        //}

        //public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetLandAndOwnersDetailsAsync(int unitId)
        //{
        //    var lands = new List<LandMapped>();
        //    var landOwners = new List<LandOwnerMapped>();

        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "[dbo].[GetLandOwnersDetails]";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.Add(new SqlParameter("@unitId", unitId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // Read LandOwner data from first result set
        //                while (await reader.ReadAsync())
        //                {
        //                    var landOwner = new LandOwnerMapped
        //                    {
        //                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
        //                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
        //                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                        AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
        //                        AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
        //                        AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
        //                        BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
        //                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
        //                        AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
        //                        AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
        //                        AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
        //                        LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
        //                        LandNotes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"))
        //                    };
        //                    landOwners.Add(landOwner);
        //                }

        //                // Move to the next result set
        //                await reader.NextResultAsync();

        //                // Read Land data from second result set
        //                while (await reader.ReadAsync())
        //                {
        //                    var land = new LandMapped
        //                    {
        //                        LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
        //                        Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
        //                        MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
        //                        SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
        //                        PlotNo = reader.IsDBNull(reader.GetOrdinal("PlotNo")) ? null : reader.GetString(reader.GetOrdinal("PlotNo")),
        //                        AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
        //                        AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
        //                        AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
        //                        TotalArea = reader.IsDBNull(reader.GetOrdinal("TotalArea")) ? 0 : reader.GetFloat(reader.GetOrdinal("TotalArea")),
        //                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
        //                        OwnershipTypeId = reader.GetInt32(reader.GetOrdinal("OwnershipTypeId")),
        //                        OwnershipType = reader.GetString(reader.GetOrdinal("OwnershipType")),
        //                        ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
        //                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
        //                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
        //                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                        UnitId = reader.GetInt32(reader.GetOrdinal("UnitId")),
        //                        Unit = reader.GetString(reader.GetOrdinal("Unit")),
        //                        LandTypeId = reader.GetInt32(reader.GetOrdinal("LandTypeId")),
        //                        ParentUId = reader.GetInt32(reader.GetOrdinal("ParentUId")),
        //                        ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
        //                    };
        //                    lands.Add(land);
        //                }

        //                // Move to the next result set
        //                await reader.NextResultAsync();

        //                // Read additional LandOwner data from third result set
        //                while (await reader.ReadAsync())
        //                {
        //                    var landOwner = new LandOwnerMapped
        //                    {
        //                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
        //                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
        //                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                        AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
        //                        AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
        //                        AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
        //                        BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
        //                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
        //                        LandId = reader.GetInt32(reader.GetOrdinal("LandId"))
        //                    };
        //                    landOwners.Add(landOwner);
        //                }
        //            }
        //        }
        //    }

        //    return (lands, landOwners);
        //}



        //public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetLandAndOwnersAsync(int unitId)
        //{
        //    var lands = new List<LandMapped>();
        //    var landOwners = new List<LandOwnerMapped>();

        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "[dbo].[GetLandOwnersDetailsUnderUnit_SP]";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.Add(new SqlParameter("@unitId", unitId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // Read Land data
        //                while (await reader.ReadAsync())
        //                {
        //                    var land = new LandMapped
        //                    {
        //                        LandId = reader.GetInt32(reader.GetOrdinal("LandId")),
        //                        Municipality = reader.GetString(reader.GetOrdinal("Municipality")),
        //                        MainNo = reader.GetString(reader.GetOrdinal("MainNo")),
        //                        SubNo = reader.GetString(reader.GetOrdinal("SubNo")),
        //                        PlotNo = reader.IsDBNull(reader.GetOrdinal("PlotNo")) ? null : reader.GetString(reader.GetOrdinal("PlotNo")),
        //                        AreaInForest = reader.GetFloat(reader.GetOrdinal("AreaInForest")),
        //                        AreaInMountain = reader.GetFloat(reader.GetOrdinal("AreaInMountain")),
        //                        AreaInAgriculture = reader.GetFloat(reader.GetOrdinal("AreaInAgriculture")),
        //                        TotalArea = reader.IsDBNull(reader.GetOrdinal("TotalArea")) ? 0 : reader.GetFloat(reader.GetOrdinal("TotalArea")),
        //                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
        //                        OwnershipTypeId = reader.GetInt32(reader.GetOrdinal("OwnershipTypeId")),
        //                        OwnershipType = reader.GetString(reader.GetOrdinal("OwnershipType")),
        //                        ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
        //                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
        //                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
        //                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                        UnitId = reader.GetInt32(reader.GetOrdinal("UnitId")),
        //                        Unit = reader.GetString(reader.GetOrdinal("Unit")),
        //                        LandTypeId = reader.GetInt32(reader.GetOrdinal("LandTypeId")),
        //                        ParentUId = reader.GetInt32(reader.GetOrdinal("ParentUId")),
        //                        ParentUnit = reader.GetString(reader.GetOrdinal("ParentUnit"))
        //                    };
        //                    lands.Add(land);
        //                }

        //                // Move to the next result set
        //                await reader.NextResultAsync();

        //                // Read LandOwner data
        //                while (await reader.ReadAsync())
        //                {
        //                    var landOwner = new LandOwnerMapped
        //                    {
        //                        LandOwnerId = reader.GetInt32(reader.GetOrdinal("LandOwnerId")),
        //                        SystemUserId = reader.GetInt32(reader.GetOrdinal("SystemUserId")),
        //                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                        ContactOwnerLandId = reader.GetInt32(reader.GetOrdinal("ContactOwnerLandId")),
        //                        AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1")),
        //                        AddressLine2 = reader.GetString(reader.GetOrdinal("AddressLine2")),
        //                        AddressCity = reader.GetString(reader.GetOrdinal("AddressCity")),
        //                        BankAccountNo = reader.GetString(reader.GetOrdinal("BankAccountNo")),
        //                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
        //                        LandId = reader.GetInt32(reader.GetOrdinal("LandId"))
        //                    };
        //                    landOwners.Add(landOwner);
        //                }
        //            }
        //        }
        //    }

        //    return (lands, landOwners);
        //}
    }
}



//public async Task<(List<LandMapped>, List<LandOwnerMapped>)> GetLandAndOwnersAsync(int unitId)
//{
//    var lands = new List<LandMapped>();
//    var landOwners = new List<LandOwnerMapped>();

//    using (var connection = _context.Database.GetDbConnection())
//    {
//        await connection.OpenAsync();

//        using (var command = connection.CreateCommand())
//        {
//            command.CommandText = "[dbo].[GetLandDetailsUnderUnit_SP]";
//            command.CommandType = System.Data.CommandType.StoredProcedure;
//            command.Parameters.Add(new SqlParameter("@unitId", unitId));

//            using (var reader = await command.ExecuteReaderAsync())
//            {
//                // Read Land data
//                while (await reader.ReadAsync())
//                {
//                    var land = new LandMapped
//                    {
//                        LandId = reader.GetInt32(0), // l.LandId
//                        Municipality = reader.GetString(1), // l.Municipality
//                        MainNo = reader.GetString(2), // l.MainNo
//                        SubNo = reader.GetString(3), // l.SubNo
//                        PlotNo = reader.GetString(4), // l.PlotNo
//                        AreaInForest = reader.GetFloat(5), // l.AreaInForest
//                        AreaInMountain = reader.GetFloat(6), // l.AreaInMountain
//                        AreaInAgriculture = reader.GetFloat(7), // l.AreaInAgriculture
//                        TotalArea = reader.GetFloat(8), // l.TotalArea
//                        Notes = reader.GetString(9), // l.Notes
//                        OwnershipTypeId = reader.GetInt32(10), // ot.OwnershipTypeId
//                        OwnershipType = reader.GetString(11), // ot.OwnershipType
//                        ContactOwnerLandId = reader.GetInt32(12), // IsNull(od.LandId, -1)
//                        LandOwnerId = reader.GetInt32(13), // lo.LandOwnerId
//                        SystemUserId = reader.GetInt32(14), // lo.SystemUserId
//                        DisplayName = reader.GetString(15), // vu.DisplayName
//                        FirstName = reader.GetString(16), // vu.FirstName
//                        LastName = reader.GetString(17), // vu.LastName
//                        Email = reader.GetString(18), // vu.Email
//                        ContactNumber = reader.GetString(19), // vu.ContactNumber
//                        UnitId = reader.GetInt32(20), // u.UnitID
//                        Unit = reader.GetString(21), // u.Unit
//                        LandTypeId = reader.GetInt32(22), // lu.LandTypeId
//                        ParentUId = reader.GetInt32(23), // parent_unit.UnitID
//                        ParentUnit = reader.GetString(24) // parent_unit.Unit
//                    };
//                    lands.Add(land);
//                }

//                // Move to the next result set
//                await reader.NextResultAsync();

//                // Read LandOwner data
//                while (await reader.ReadAsync())
//                {
//                    var landOwner = new LandOwnerMapped
//                    {
//                        LandOwnerId = reader.GetInt32(0), // lo.LandOwnerId
//                        SystemUserId = reader.GetInt32(1), // lo.SystemUserId
//                        DisplayName = reader.GetString(2), // vu.DisplayName
//                        FirstName = reader.GetString(3), // vu.FirstName
//                        LastName = reader.GetString(4), // vu.LastName
//                        FullName = reader.GetString(5), // FullName (concatenated)
//                        Email = reader.GetString(6), // vu.Email
//                        ContactNumber = reader.GetString(7), // vu.ContactNumber
//                        ContactOwnerLandId = reader.GetInt32(8), // IsNull(od.LandId, -1)
//                        AddressLine1 = reader.GetString(9), // od.AddressLine1
//                        AddressLine2 = reader.GetString(10), // od.AddressLine2
//                        AddressCity = reader.GetString(11), // od.AddressCity
//                        BankAccountNo = reader.GetString(12), // od.BankAccountNo
//                        Notes = reader.GetString(13), // od.Notes
//                        LandId = reader.GetInt32(14) // l.LandId
//                    };
//                    landOwners.Add(landOwner);
//                }
//            }
//        }
//    }

//    return (lands, landOwners);
//}

