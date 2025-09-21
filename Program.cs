using CouponAPI.Data;
using Microsoft.AspNetCore.Mvc;
using CouponAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/coupon", () => Results.Ok(CouponStore.couponList));
app.MapGet("/api/coupon/{id:int}", (int id) => Results.Ok(CouponStore.couponList.FirstOrDefault(u => u.Id == id)));
app.MapPost("/api/coupon", ([FromBody] Coupon coupon) =>
{
    if (coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
        return Results.BadRequest("Invalid Id or Coupon Name");
    if (CouponStore.couponList.FirstOrDefault(u => u.Name.ToLower() == coupon.Name.ToLower()) != null)
        return Results.BadRequest("Coupon Name already Exists");
    var last = CouponStore.couponList.OrderByDescending(u => u.Id).FirstOrDefault();
    coupon.Id = last != null ? last.Id + 1 : 0;
    CouponStore.couponList.Add(coupon);
    return Results.Ok(coupon);
});
app.MapPut("/api/coupon", () => { });
app.MapDelete("/api/coupon{id:int}", (int id) => { } );


app.UseHttpsRedirection();

app.Run();

//Lesson 20