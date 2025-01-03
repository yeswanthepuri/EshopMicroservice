using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Arguments"));
            }
            discountContext.Add<Coupon>(coupon);
            await discountContext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }
            discountContext.Remove<Coupon>(coupon);
            await discountContext.SaveChangesAsync();

            return new DeleteDiscountResponse { Success = true };
        }
        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new Models.Coupon { ProductName = "No discount", Amount = 0, Description = "0 Discount applied", Id = 0 };
            }
            return coupon.Adapt<CouponModel>();
        }
        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Arguments"));
            }
            discountContext.Update<Coupon>(coupon);
            await discountContext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
    }
}
