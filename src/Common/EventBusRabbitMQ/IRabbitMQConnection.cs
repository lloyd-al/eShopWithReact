﻿using System;
using RabbitMQ.Client;

namespace eShopWithReact.Common.EventBusRabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
