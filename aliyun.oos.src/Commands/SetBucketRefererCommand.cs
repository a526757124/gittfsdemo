﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System;
using System.Collections.Generic;
using System.IO;
using Aliyun.OpenServices.Common.Communication;
using Aliyun.OpenServices.OpenStorageService.Transform;
using Aliyun.OpenServices.OpenStorageService.Utilities;
using Aliyun.OpenServices.Utilities;

namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    /// <summary>
    /// Description of SetBucketRefererCommand.
    /// </summary>
    internal class SetBucketRefererCommand : OssCommand
    {
        private readonly string _bucketName;
        private readonly SetBucketRefererRequest _setBucketRefererRequest;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Put; }
        }

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        protected override Stream Content
        {
            get
            {
                return SerializerFactory.GetFactory().CreateSetBucketRefererRequestSerializer()
                    .Serialize(_setBucketRefererRequest);
            }
        }

        private SetBucketRefererCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName, SetBucketRefererRequest setBucketRefererRequest)
            : base(client, endpoint, context)
        {
            OssUtils.CheckBucketName(bucketName);

            _bucketName = bucketName;
            _setBucketRefererRequest = setBucketRefererRequest;
        }

        public static SetBucketRefererCommand Create(IServiceClient client, Uri endpoint,
                                                 ExecutionContext context,
                                                 string bucketName, SetBucketRefererRequest setBucketRefererRequest)
        {
            return new SetBucketRefererCommand(client, endpoint, context, bucketName, setBucketRefererRequest);
        }


        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_REFERER, null }
                };
            }
        }
    }
}
