namespace ViltrapportenApi.Services
{
    /// <summary>
    /// Service for managing land and land owner information.
    /// </summary>

    public class ExampleDocumentationLandService
    {
     
            /// <summary>
            /// Retrieves all lands under a specified unit.
            /// </summary>
            /// <param name="unitId">The identifier of the unit.</param>
            /// <returns>A list of lands mapped to the specified unit.</returns>
            /// <remarks>
            /// This method interacts with the database to fetch lands based on the provided unit ID.
            /// It ensures that only active lands are retrieved.
            /// </remarks>
            public async Task<List<LandMappedExample>> GetLandsByUnitAsync(int unitId)
            {
                // Implementation goes here
                return new List<LandMappedExample>();
            }

            /// <summary>
            /// Retrieves all owners for a given land.
            /// </summary>
            /// <param name="landId">The identifier of the land.</param>
            /// <returns>A list of owners associated with the land.</returns>
            /// <remarks>
            /// Use this method to fetch ownership details, including shared ownership information.
            /// </remarks>
            public async Task<List<LandMappedExample>> GetLandOwnersAsync(int landId)
            {
                // Implementation goes here
                return new List<LandMappedExample>();
            }
        }

        /// <summary>
        /// Represents mapped information about a land.
        /// </summary>
        public class LandMappedExample
    {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Represents mapped information about a land owner.
        /// </summary>
        public class LandOwnerMappedExample
    {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
    }

