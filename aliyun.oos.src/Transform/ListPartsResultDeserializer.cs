﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */
 
using System;
using System.IO;
using Aliyun.OpenServices.Common.Communication;
using Aliyun.OpenServices.Common.Transform;
using Aliyun.OpenServices.OpenStorageService.Model;
using Aliyun.OpenServices.OpenStorageService.Utilities;

namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    internal class ListPartsResponseDeserializer : ResponseDeserializer<PartListing, ListPartsResult>
    {
        public ListPartsResponseDeserializer(IDeserializer<Stream, ListPartsResult> contentDeserializer)
            : base(contentDeserializer)
        { }
        
        public override PartListing Deserialize(ServiceResponse xmlStream)
        {
            var listPartResult = ContentDeserializer.Deserialize(xmlStream.Content);
            
            var partListing = new PartListing
            {
                BucketName = listPartResult.Bucket,
                Key = listPartResult.Key,
                MaxParts = listPartResult.MaxParts,
                NextPartNumberMarker = listPartResult.NextPartNumberMarker.Length == 0 ? 
                    0 : Convert.ToInt32(listPartResult.NextPartNumberMarker),
                PartNumberMarker = listPartResult.PartNumberMarker,
                UploadId = listPartResult.UploadId,
                IsTruncated = listPartResult.IsTruncated
            };

            if (listPartResult.PartResults != null)
            {
                foreach (var partResult in listPartResult.PartResults)
                {
                    var part = new Part
                    {
                        ETag = partResult.ETag != null ? OssUtils.TrimQuotes(partResult.ETag) : string.Empty,
                        LastModified = partResult.LastModified,
                        PartNumber = partResult.PartNumber,
                        Size = partResult.Size
                    };
                    partListing.AddPart(part);
                }
            }

            return partListing;
        }
    }
}
