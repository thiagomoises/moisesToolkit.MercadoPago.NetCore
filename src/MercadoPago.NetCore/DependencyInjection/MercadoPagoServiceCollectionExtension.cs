﻿using AutoMapper;
using MercadoPago.NetCore.AutoMapper;
using MercadoPago.NetCore.Model.Resources;
using Microsoft.Extensions.DependencyInjection;
using Moises.Toolkit.MercadoPago.NetCore.HubClients;
using Moises.Toolkit.MercadoPago.NetCore.HubClients.Abstracts;
using System;
using System.Net.Http;

namespace Moises.Toolkit.MercadoPago.NetCore.DependencyInjection
{
    public static class MercadoPagoServiceCollectionExtension
    {
        public static IServiceCollection AddMercadoPago(this IServiceCollection services, Action<MPOptions> setupAction = null)
        {
            var option = new MPOptions();
            setupAction?.Invoke(option);
            services.AddSingleton(option);

            services.AddHttpClient("mercadopago", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.mercadopago.com");
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "MercadoPago DotNet SDK/1.0.30");
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<ICardsHubClient, CardsHubClient>();
            services.AddTransient<ICustomerHubClient, CustomerHubClient>();
            services.AddTransient<IMerchantOrderHubClient, MerchantOrderHubClient>();
            services.AddTransient<IPaymentHubClient, PaymentHubClient>();
            services.AddTransient<IPaymentMethodHubClient, PaymentMethodHubClient>();
            services.AddTransient<IPlanHubClient, PlanHubClient>();
            services.AddTransient<IPreapprovalHubClient, PreapprovalHubClient>();
            services.AddTransient<IPreferenceHubClient, PreferenceHubClient>();
            services.AddTransient<IRefundHubClient, RefundHubClient>();
            services.AddTransient<ITokenHubClient, TokenHubClient>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
