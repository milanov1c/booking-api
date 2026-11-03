using Booking.Application;
//using Booking.Application.Commands;
using Booking.API.JWT;
using Booking.DataAccess;
using FluentValidation.Results;
using Booking.DataAccess.Seeding;
using Booking.Implementation;
using Booking.Implementation.UseCases;
//using Booking.Implementation.UseCases.Commands;
//using Booking.Implementation.UseCases.Queries;
//using Implementation.UseCases.Validators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Booking.API.ExceptionLogging;
using Booking.API.JWT;
using Booking.Application.Commands;
using Booking.Application.Queries;
using Booking.Implementation.UseCases.Commands;
using Booking.Implementation.UseCases.Queries;
using Booking.Implementation.Validators;
using Booking.Implementation.Storage;

namespace Booking.API
{
    public static class ApiExtensions
    {
        public static bool IsLocal(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == "Development";
        }

        public static void SetupApplication(this IServiceCollection services, AppSettings settings)
        {
            services.AddSingleton(settings);
            services.AddTransient(x => new BookingDbContext(settings.ConnString));
            services.AddTransient<IApplicationUser, UnauthorizedUser>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RoleValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<JwtHandler>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IAddCountryCommand, EfAddCountryCommand>();
            services.AddTransient<IAddDestinationCommand, EfAddDestinationCommand>();
            services.AddTransient<IAddUseCaseCommand, EfAddUseCaseCommand>();
            services.AddTransient<IAttachUseCaseCommand, EfAttachUseCaseCommand>();
            services.AddTransient<IAddPropertyCommand, EfAddPropertyCommand>();
            services.AddTransient<IAddPropertyRoomCommand, EfAddPropertyRoomCommand>();
            services.AddTransient<IAttachPropertyRoomImagesCommand, EfAttachPropertyRoomImagesCommand>();
            services.AddTransient<IAttachPropertyImagesCommand, EfAttachPropertyImagesCommand>();
            services.AddTransient<IAddReservationCommand, EfAddReservationCommand>();
            services.AddTransient<ISearchPropertiesQuery, EfSearchPropertiesQuery>();
            services.AddTransient<IGetPropertyQuery, EfGetPropertyQuery>();
            services.AddTransient<IGetPropertyRoomQuery, EfGetPropertyRoomQuery>();
            services.AddTransient<IGetUserReservationsQuery, EfGetUserReservationsQuery>();
            services.AddTransient<IGetUserProfileQuery, EfGetUserProfileQuery>();
            services.AddTransient<IAddAmenityCommand, EfAddAmenityCommand>();
            services.AddTransient<AddAmenityValidator>();
            services.AddTransient<IGetCountriesQuery, EfGetCountriesQuery>();
            services.AddTransient<IGetDestinationsQuery, EfGetDestinationsQuery>();
            services.AddTransient<IGetPropertyTypesQuery, EfGetPropertyTypesQuery>();
            services.AddTransient<IGetRolesQuery, EfGetRolesQuery>();
            services.AddTransient<IGetUseCasesQuery, EfGetUseCasesQuery>();
            services.AddTransient<IGetAmenitiesQuery, EfGetAmenitiesQuery>();
            services.AddTransient<AddCountryValidator>();
            services.AddTransient<AddDestinationValidator>();
            services.AddTransient<AddUseCaseValidator>();
            services.AddTransient<AttachUseCaseValidator>();
            services.AddTransient<AddPropertyValidator>();
            services.AddTransient<AddPropertyRoomValidator>();
            services.AddTransient<AttachPropertyRoomImagesValidator>();
            services.AddTransient<AttachPropertyImagesValidator>();
            services.AddTransient<AddReservationValidator>();
            services.AddTransient<IFileStorage>(_ => new LocalFileStorage());
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
            services.AddTransient<IAttachPriceCommand, EfAttachPriceCommand>();
            services.AddTransient<AttachPriceValidator>();
            services.AddTransient<ConfirmReservationValidator>();
            services.AddTransient<IConfirmReservationCommand, EfConfirmReservation>();
            services.AddTransient<CancelReservationValidator>();
            services.AddTransient<ICancelReservationCommand, EfCancelReservationCommand>();
            services.AddTransient<AddReviewValidator>();
            services.AddTransient<IAddReviewCommand, EfAddReviewCommand>();
            services.AddTransient<IGetPropertyReviewsQuery, EfGetPropertyReviewsQuery>();
            services.AddTransient<AttachAmenityValidator>();
            services.AddTransient<IAttachAmenityCommand, EfAttachAmenityCommand>();
            services.AddTransient<IGetLandlordReservationsQuery, EfGetLandlordReservationsQuery>();

            services.AddTransient<IUpdatePropertyCommand, EfUpdatePropertyCommand>();
            services.AddTransient<UpdatePropertyValidator>();
            services.AddTransient<IDeletePropertyCommand, EfDeletePropertyCommand>();
            services.AddTransient<DeletePropertyValidator>();
            services.AddTransient<IUpdatePropertyRoomCommand, EfUpdatePropertyRoomCommand>();
            services.AddTransient<UpdatePropertyRoomValidator>();
            services.AddTransient<IDeletePropertyRoomCommand, EfDeletePropertyRoomCommand>();
            services.AddTransient<DeletePropertyRoomValidator>();
            services.AddTransient<IDeleteAttachmentCommand, EfDeleteAttachmentCommand>();
            services.AddTransient<DeleteAttachmentValidator>();
            services.AddTransient<ICheckPropertyRoomAvailabilityQuery, EfCheckPropertyRoomAvailabilityQuery>();
            services.AddTransient<IGetAuditLogsQuery, EfGetAuditLogsQuery>();
            services.AddTransient<IUpdatePropertyRoomPriceCommand, EfUpdatePropertyRoomPriceCommand>();
            services.AddTransient<UpdatePropertyRoomPriceValidator>();
            services.AddTransient<IDeletePropertyRoomPriceCommand, EfDeletePropertyRoomPriceCommand>();
            services.AddTransient<DeletePropertyRoomPriceValidator>();
            services.AddTransient<IGetPropertyRoomPricesQuery, EfGetPropertyRoomPricesQuery>();
            services.AddTransient<IDetachAmenityCommand, EfDetachAmenityCommand>();
            services.AddTransient<DetachAmenityValidator>();
            services.AddTransient<IUpdateReviewCommand, EfUpdateReviewCommand>();
            services.AddTransient<UpdateReviewValidator>();
            services.AddTransient<IDeleteReviewCommand, EfDeleteReviewCommand>();
            services.AddTransient<DeleteReviewValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IUpdateUserRoleCommand, EfUpdateUserRoleCommand>();
            services.AddTransient<UpdateUserRoleValidator>();
            services.AddTransient<IUpdateUserStatusCommand, EfUpdateUserStatusCommand>();
            services.AddTransient<UpdateUserStatusValidator>();
            services.AddTransient<IDetachUseCaseCommand, EfDetachUseCaseCommand>();
            services.AddTransient<DetachUseCaseValidator>();
            services.AddTransient<IGetLandlordStatsQuery, EfGetLandlordStatsQuery>();
            services.AddTransient<IChangePasswordCommand, EfChangePasswordCommand>();
            services.AddTransient<ChangePasswordValidator>();

            services.AddDatabaseSeeding(settings.Seed);

            services.AddTransient<IApplicationUser>(container =>
            {
                var accessor = container.GetService<IHttpContextAccessor>();

                if(accessor.HttpContext == null)
                {
                    return new UnauthorizedUser();
                }

                if (!accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new UnauthorizedUser();
                }
                
                var header = accessor.HttpContext.Request.Headers.Authorization; //Bearer token
                var headerParts = header.ToString().Split(" ");
                
                if(headerParts.Count() != 2 || headerParts[0] != "Bearer")
                {
                    return new UnauthorizedUser();
                }

                var token = headerParts[1];

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                //jwtToken.Claims

                var useCaseIdsClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "UseCaseIds")?.Value;
                var allowedUseCases = string.IsNullOrEmpty(useCaseIdsClaim)
                    ? new List<string>()
                    : useCaseIdsClaim.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                return new JwtUser
                {
                    Id = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value),
                    Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email").Value,
                    Role = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                    AllowedUseCases = allowedUseCases,
                };
            });
        }
    }
}
