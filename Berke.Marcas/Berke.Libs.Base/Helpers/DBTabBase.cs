using System;
using System.Data;
using Berke.Libs.Base;
using Berke.Libs.Base.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.DG.Adapters;


#region Clases Base

namespace Berke.DG
{
	namespace Base 
	{
		#region Field types

		#region Field

		public class Field 
		{
			#region Datos miembro
			
			protected  DSTab _dst;
			protected  int _idx;
			protected  bool _oldDataVersion;

			#endregion Datos miembro

			#region Constructor 

			public Field( DSTab dst, int idx , bool oldDataVersion )
			{
				_dst = dst;
				_idx = idx;
				_oldDataVersion = oldDataVersion;
			}

			#endregion Constructor

			#region Propiedades
				
			#region Name
			public string Name 
			{
				get 
				{
					return _dst.Table.Columns[_idx].ColumnName;
				}
			}				
			#endregion 	
						
			#region IsNull
			public bool IsNull 
			{
				get 
				{
					return Berke.Libs.Base.ObjConvert.IsNull( _dst.Dat[_idx]);
				}
			}				
			#endregion 				
				
			#region IsValueChanged
			public bool IsValueChanged 
			{
				get 
				{
					return 0 != Berke.Libs.Base.ObjConvert.Compare( _dst.Old[_idx], _dst.Dat[_idx]);
				}
			}				
			#endregion 				

			#region Value
			public object Value 
			{
				set 
				{
					_dst.Dat[_idx] = value;	
				}
				get 
				{
					if( _oldDataVersion )
						return _dst.Old[_idx];	
					else
						return _dst.Dat[_idx];	
				}
			}
			#endregion 				

			#region AsString
			public string AsString 
			{
				get { return _dst.AsString(_idx,_oldDataVersion);	}
			}
			#endregion 				

			#region Filter
			public object Filter 
			{
				set { _dst.Filter[_idx] = value; }
			}
			#endregion 				

			#region Order
			public int Order
			{
				set { _dst.SetOrder(_idx, value );}
			}
			#endregion 				

			#endregion Propiedades
				
			#region Metodos
				
			#region SetNull
			public void SetNull()
			{
				_dst.Dat[_idx] = DBNull.Value;	
			}
			#endregion 
								
			#region SetValue
			public void SetValue( object valor )
			{
				object obj = new object();
				ObjConvert.SetValue( ref obj , valor, _dst.Dat.Column[_idx].ColType );
				_dst.Dat[_idx] = obj;
			}
			#endregion 			
									
			#endregion Metodos

		}
		#endregion Field
		
		#region Boolean_Field
		public class Boolean_Field : Field	
		{
			public Boolean_Field( DSTab dst, int idx , bool oldDataVersion) : base( dst, idx, oldDataVersion ){}
			public Boolean AsBoolean {	get { return _dst.AsBoolean(_idx,_oldDataVersion);	}	}
		}
		#endregion Boolean_Field

		#region Binary_Field
		public class Binary_Field : Field	
		{
			public Binary_Field( DSTab dst, int idx , bool oldDataVersion) : base( dst, idx , oldDataVersion){}
			public Byte[] AsBinary {	get { return _dst.AsBinary(_idx,_oldDataVersion);	}	}
		}
		#endregion Binary_Field

		#region Byte_Field
		public class Byte_Field : Field	
		{
			public Byte_Field( DSTab dst, int idx , bool oldDataVersion) : base( dst, idx , oldDataVersion){}
			public Byte AsByte {	get { return _dst.AsByte(_idx,_oldDataVersion);	}	}
		}
		#endregion Byte_Field

		#region DateTime_Field
		public class DateTime_Field : Field	
		{
			public DateTime_Field( DSTab dst, int idx , bool oldDataVersion) : base( dst, idx , oldDataVersion){}
			public DateTime AsDateTime {	get { return _dst.AsDateTime(_idx,_oldDataVersion);	}	}
		}
		#endregion DateTime_Field

		#region Decimal_Field
		public class Decimal_Field : Field	
		{
			public Decimal_Field( DSTab dst, int idx, bool oldDataVersion ) : base( dst, idx, oldDataVersion ){}
			public Decimal AsDecimal {	get { return _dst.AsDecimal(_idx,_oldDataVersion);	}	}
		}
		#endregion Decimal_Field

		#region Double_Field
		public class Double_Field : Field	
		{
			public Double_Field( DSTab dst, int idx, bool oldDataVersion ) : base( dst, idx ,oldDataVersion ){}
			public Double AsDouble {	get { return _dst.AsDouble(_idx,_oldDataVersion);	}	}
		}
		#endregion Double_Field

		#region Single_Field
		public class Single_Field : Field	
		{
			public Single_Field( DSTab dst, int idx , bool oldDataVersion) : base( dst, idx, oldDataVersion ){}
			public Single AsSingle {	get { return _dst.AsSingle(_idx,_oldDataVersion);	}	}
		}
		#endregion Single_Field

		#region Short_Field
		public class Short_Field : Field	
		{
			public Short_Field( DSTab dst, int idx, bool oldDataVersion ) : base( dst, idx, oldDataVersion ){}
			public short AsShort {	get { return _dst.AsShort(_idx,_oldDataVersion);	}	}
		}
		#endregion Short_Field

		#region Int_Field
		public class Int_Field : Field	
		{
			public Int_Field( DSTab dst, int idx, bool oldDataVersion ) : base( dst, idx,oldDataVersion ){}
			public int AsInt {	get { return _dst.AsInt(_idx,_oldDataVersion);	}	}
		}
		#endregion Int_Field

		#region Long_Field
		public class Long_Field : Field	
		{
			public Long_Field( DSTab dst, int idx , bool oldDataVersion ) : base( dst, idx , oldDataVersion){}
			public long AsLong {	get { return _dst.AsLong(_idx,_oldDataVersion);	}	}
		}
		#endregion Long_Field

		#region String_Field
		public class String_Field : Field	
		{
			public String_Field( DSTab dst, int idx , bool oldDataVersion ) : base( dst, idx, oldDataVersion ){}
		}
		#endregion String_Field

		#endregion Field types
		
		
		//----------		
		

		#region ViewBase

		public class ViewBase
		{
			#region Dato Miembro
			public string _dbTableName="";
			protected  DSTab _dst;
			protected ViewAdapter _adapter;

			#endregion Dato Miembro

			#region Constructor 

			public ViewBase()
			{
				_dst = null;
			}

			#endregion Constructor

			#region Propiedades 

			public ViewAdapter Adapter
			{
				get
				{ 
					if ( this._adapter == null ) throw new Exception("Adapter Nulo");
					return _adapter;   
				}
			}
			public bool			EOF			{ get { return _dst.EOF;		}}
			public bool			IsEmpty		{ get { return _dst.IsEmpty;	}}
			public int			RowCount	{ get { return _dst.RowCount;	}}
			public int			RowIndex	{ get { return _dst.RowIndex;	}}
			public DataTable	Table		{ get { return _dst.Table;		} set {this._dst.Table = value;}}
			
			public DataRowState	RowState	{ get { return _dst.RowState;	}}

			public DSTab	DST	{ get { return _dst;	}}

			public bool IsRowAdded		{ get{	return _dst.IsRowAdded ;	}}
			public bool IsRowDeleted	{ get{	return _dst.IsRowDeleted ;	}}
			public bool IsRowModified	{ get{	return _dst.IsRowModified;  }}
		
			#endregion Propiedades 

			#region Metodos

			public virtual void InitAdapter( AccesoDB db )
			{
				_adapter = new ViewAdapter( this, db );
			}


			public void AcceptAllChanges()	{	_dst.AcceptAllChanges();	}
			public void	AcceptRowChanges()	{	_dst.AcceptRowChanges();	}
			public void	CancelEdit()		{	_dst.CancelEdit();			}
			public void	Delete()			{	_dst.Delete();				}
			public void	Edit()				{	_dst.Edit();				}
			public void	GoTop()				{	_dst.GoTop();				}
			public void	Go( int pos )		{	_dst.Go( pos );				}			
			public void	ClearOrder()		{	_dst.ClearOrder();			}
			public void	ClearFilter()		{	_dst.ClearFilter();			}
			public void	PostEdit()			{	_dst.PostEdit();			}
			public void	FilterOn()			{	_dst.FilterOn();			}
			public void	FilterOff()			{	_dst.FilterOff();			}
			public void	Skip()				{	_dst.Skip();				}
			public void	Skip( int inc )		{	_dst.Skip(inc);				}
			public void	Sort()				{	_dst.Sort();				}
		
			public void NewRow(){ _dst.NewRow(); }
			public void PostNewRow(){ _dst.PostNewRow(); }

			#region DeleteAllRows
			public void	DeleteAllRows()
			{ // Borra las que cumplen el filtro
				for( _dst.GoTop(); !_dst.EOF; _dst.Skip() )
				{ 
					_dst.Delete();
				}
			}			
			#endregion DeleteAllRows

			#region AsDataSet
			public DataSet AsDataSet()
			{
				DataSet  tmpDS	= new DataSet(); 
				tmpDS.Tables.Add( _dst.Table.Copy() );
				return tmpDS;
			}
			#endregion
		
			#endregion Metodos
		}
	
		#endregion ViewBase

		
		//----------
		#region TableBase

		public class TableBase
		{
			#region Dato Miembro

			protected  DSTab _dst;
			protected AdapterBase _adapter;
			public string _dbTableName="";
			#endregion Dato Miembro

			#region Constructor 

			public TableBase()
			{
				_dst = null;
			}

			#endregion Constructor

			#region Propiedades 

			public AdapterBase Adapter
			{
				get
				{ 
					if ( this._adapter == null ) throw new Exception("Adapter Nulo");
					return _adapter;   
				}
			}
			public bool			EOF			{ get { return _dst.EOF;		}}
			public bool			IsEmpty		{ get { return _dst.IsEmpty;	}}
			public int			RowCount	{ get { return _dst.RowCount;	}}
			public int			RowIndex	{ get { return _dst.RowIndex;	}}
			public DataTable	Table		{	get { return _dst.Table;}	
				set {	_dst.Table = value;	}
			}

			public DataRowState	RowState	{ get { return _dst.RowState;	}}

			public DSTab	DST	{ get { return _dst;	}}

			public bool IsRowAdded		{ get{	return _dst.IsRowAdded ;	}}
			public bool IsRowDeleted	{ get{	return _dst.IsRowDeleted ;	}}
			public bool IsRowModified	{ get{	return _dst.IsRowModified;  }}
	
			#endregion Propiedades 

			#region Metodos

			public void DisableConstraints(){
				_dst.DisableConstraints();
			}

			public virtual void InitAdapter( AccesoDB db )
			{
				_adapter = new AdapterBase( this, db );
			}

			public void AcceptAllChanges()	{	_dst.AcceptAllChanges();	}
			public void	AcceptRowChanges()	{	_dst.AcceptRowChanges();	}
			public void	CancelEdit()		{	_dst.CancelEdit();			}
			public void	Delete()			{	_dst.Delete();				}
			public void	Edit()				{	_dst.Edit();				}
			public void	GoTop()				{	_dst.GoTop();				}
			public void	Go( int pos )		{	_dst.Go( pos );				}			
			public void	ClearOrder()		{	_dst.ClearOrder();			}
			public void	ClearFilter()		{	_dst.ClearFilter();			}
			public void	PostEdit()			{	_dst.PostEdit();			}
			public void	FilterOn()			{	_dst.FilterOn();			}
			public void	FilterOff()			{	_dst.FilterOff();			}
			public void	Skip()				{	_dst.Skip();				}
			public void	Skip( int inc )		{	_dst.Skip(inc);				}
			public void	Sort()				{	_dst.Sort();				}
		
			public void NewRow(){ _dst.NewRow(); }
			public void PostNewRow(){ _dst.PostNewRow(); }

			#region DeleteAllRows
			public void	DeleteAllRows()
			{ // Borra las que cumplen el filtro
				for( _dst.GoTop(); !_dst.EOF; _dst.Skip() )
				{ 
					_dst.Delete();
				}
			}			
			#endregion

			#region AsDataSet
			public DataSet AsDataSet(){
				DataSet  tmpDS	= new DataSet(); 
				tmpDS.Tables.Add( _dst.Table.Copy() );
				return tmpDS;
			}
			#endregion

			#endregion Metodos
		}
	
		#endregion TableBase

	} //	namespace Base
} // Berke.DG

#endregion Clases Base
