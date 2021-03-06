﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Core;

namespace Core	
{
	public partial class EntitiesModel1 : OpenAccessContext, IEntitiesModel1UnitOfWork
	{
		private static string connectionStringName = @"CISC474ConnectionProject3";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
				
		private static MetadataSource metadataSource = XmlMetadataSource.FromAssemblyResource("EntitiesModel1.rlinq");
		
		public EntitiesModel1()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public EntitiesModel1(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public EntitiesModel1(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public EntitiesModel1(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public EntitiesModel1(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }
			
		public IQueryable<User> Users 
		{
			get
			{
				return this.GetAll<User>();
			}
		}
		
		public IQueryable<Chat> Chats 
		{
			get
			{
				return this.GetAll<Chat>();
			}
		}
		
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MsSql";
			backend.ProviderName = "System.Data.SqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of EntitiesModel1.
		/// </summary>
		/// <param name="config">The BackendConfiguration of EntitiesModel1.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface IEntitiesModel1UnitOfWork : IUnitOfWork
	{
		IQueryable<User> Users
		{
			get;
		}
		IQueryable<Chat> Chats
		{
			get;
		}
	}
}
#pragma warning restore 1591
