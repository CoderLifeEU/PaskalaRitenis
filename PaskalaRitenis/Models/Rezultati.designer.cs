﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PaskalaRitenis.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DU_IK_PaskalaRitenisNew")]
	public partial class RezultatiDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertRezultati(Rezultati instance);
    partial void UpdateRezultati(Rezultati instance);
    partial void DeleteRezultati(Rezultati instance);
    partial void InsertArchive(Archive instance);
    partial void UpdateArchive(Archive instance);
    partial void DeleteArchive(Archive instance);
    #endregion
		
		public RezultatiDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DU_IK_PaskalaRitenisNewConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public RezultatiDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RezultatiDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RezultatiDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RezultatiDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Rezultati> Rezultatis
		{
			get
			{
				return this.GetTable<Rezultati>();
			}
		}
		
		public System.Data.Linq.Table<Archive> Archives
		{
			get
			{
				return this.GetTable<Archive>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Rezultati")]
	public partial class Rezultati : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _Gads;
		
		private bool _Publicets;
		
		private string _RezultatiLink;
		
		private bool _Arhivets;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnGadsChanging(int value);
    partial void OnGadsChanged();
    partial void OnPublicetsChanging(bool value);
    partial void OnPublicetsChanged();
    partial void OnRezultatiLinkChanging(string value);
    partial void OnRezultatiLinkChanged();
    partial void OnArhivetsChanging(bool value);
    partial void OnArhivetsChanged();
    #endregion
		
		public Rezultati()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gads", DbType="Int NOT NULL")]
		public int Gads
		{
			get
			{
				return this._Gads;
			}
			set
			{
				if ((this._Gads != value))
				{
					this.OnGadsChanging(value);
					this.SendPropertyChanging();
					this._Gads = value;
					this.SendPropertyChanged("Gads");
					this.OnGadsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Publicets", DbType="Bit NOT NULL")]
		public bool Publicets
		{
			get
			{
				return this._Publicets;
			}
			set
			{
				if ((this._Publicets != value))
				{
					this.OnPublicetsChanging(value);
					this.SendPropertyChanging();
					this._Publicets = value;
					this.SendPropertyChanged("Publicets");
					this.OnPublicetsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RezultatiLink", DbType="NVarChar(MAX)")]
		public string RezultatiLink
		{
			get
			{
				return this._RezultatiLink;
			}
			set
			{
				if ((this._RezultatiLink != value))
				{
					this.OnRezultatiLinkChanging(value);
					this.SendPropertyChanging();
					this._RezultatiLink = value;
					this.SendPropertyChanged("RezultatiLink");
					this.OnRezultatiLinkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Arhivets", DbType="Bit NOT NULL")]
		public bool Arhivets
		{
			get
			{
				return this._Arhivets;
			}
			set
			{
				if ((this._Arhivets != value))
				{
					this.OnArhivetsChanging(value);
					this.SendPropertyChanging();
					this._Arhivets = value;
					this.SendPropertyChanged("Arhivets");
					this.OnArhivetsChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Archive")]
	public partial class Archive : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _Year;
		
		private string _FileName;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnYearChanging(int value);
    partial void OnYearChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    #endregion
		
		public Archive()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int NOT NULL")]
		public int Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this.OnYearChanging(value);
					this.SendPropertyChanging();
					this._Year = value;
					this.SendPropertyChanged("Year");
					this.OnYearChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if ((this._FileName != value))
				{
					this.OnFileNameChanging(value);
					this.SendPropertyChanging();
					this._FileName = value;
					this.SendPropertyChanged("FileName");
					this.OnFileNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
