using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace MovieManagementApp;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()  // في بيئة التطوير
#else
            .MinimumLevel.Information()  // في بيئة الإنتاج
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)  // تقليل السجلات الخاصة بـ Microsoft
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)  // تقليل سجلات EF Core
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File(
                "Logs/logs-.txt",
                rollingInterval: RollingInterval.Day,  // سجل يومي
                fileSizeLimitBytes: 10 * 1024 * 1024,  // حد الحجم 10 ميجابايت
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 7  // الاحتفاظ بسجلات آخر 7 أيام
            ))
            .WriteTo.Async(c => c.Console())  // كتابة السجلات إلى الـ console
            .CreateLogger();

        try
        {
            Log.Information("Starting MovieManagementApp.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();  // استخدام Serilog

            await builder.AddApplicationAsync<MovieManagementAppHttpApiHostModule>();

            var app = builder.Build();

            app.UseSerilogRequestLogging();  // إضافة تعقب الطلبات
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();  // إنهاء السجلات بشكل صحيح
        }
    }
}
