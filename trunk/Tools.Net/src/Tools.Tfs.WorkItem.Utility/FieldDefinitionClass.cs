using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Tfs.WorkItem.Utility
{
internal class FieldDefinitionClass
{
    // Fields
    private unsafe CLocker* m_lock;
    private unsafe IFieldDefinition* m_p;

    // Methods
    private unsafe void !FieldDefinitionClass()
    {
        IFieldDefinition* p = this.m_p;
        if (p != null)
        {
            IUnknown* unknownPtr = (IUnknown*) p;
            CProdStudioObjectRoot* rootPtr = __RTDynamicCast((void*) unknownPtr, 0, (void*) &??_R0?AUIUnknown@@@8, (void*) &??_R0?AVCProdStudioObjectRoot@@@8, 0);
            if (rootPtr != null)
            {
                CProdStudioObjectRoot.RegisterToRelease(rootPtr, unknownPtr);
            }
            this.m_p = null;
        }
        CLocker* @lock = this.m_lock;
        if (@lock != null)
        {
            CPslObject* modopt(IsConst) modopt(IsConst) objPtr = (CPslObject* modopt(IsConst) modopt(IsConst)) @lock;
            if ((InterlockedDecrement(objPtr + 4) == 0) && (objPtr != null))
            {
                **(objPtr[0])(objPtr, 1);
            }
            this.m_lock = null;
        }
    }

    private unsafe FieldDefinitionClass(IFieldDefinition* p)
    {
        CLocker* lockerPtr;
        this.m_p = p;
        this.m_lock = null;
        **(((int*) p))[4](p);
        Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.SetManagedProxy((IUnknown*) p, this);
        CProdStudioObjectRoot* rootPtr = __RTDynamicCast((void*) p, 0, (void*) &??_R0?AUIUnknown@@@8, (void*) &??_R0?AVCProdStudioObjectRoot@@@8, 0);
        if (rootPtr != null)
        {
            lockerPtr = *((CLocker**) (rootPtr + 12));
        }
        else
        {
            lockerPtr = null;
        }
        this.m_lock = lockerPtr;
        InterlockedIncrement((volatile int modopt(IsLong)*) (lockerPtr + 4));
    }

    public static unsafe FieldDefinitionClass CreateManagedProxy(IFieldDefinition* p)
    {
        FieldDefinitionClass class2 = null;
        if (p != null)
        {
            ErrorContext e = Context.Enter();
            try
            {
                class2 = (FieldDefinitionClass) Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.GetManagedProxy((IUnknown*) p);
                if (class2 == null)
                {
                    class2 = new FieldDefinitionClass(p);
                }
            }
            finally
            {
                Context.Exit(e);
            }
        }
        return class2;
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool flag1)
    {
        if (!flag1)
        {
            try
            {
                this.!FieldDefinitionClass();
            }
            finally
            {
                base.Finalize();
            }
        }
    }

    protected override void Finalize()
    {
        this.Dispose(false);
    }

    public unsafe IFieldDefinition* GetNativePtr()
    {
        return this.m_p;
    }

    public unsafe void LockEnter()
    {
        EnterCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
    }

    public unsafe void LockExit()
    {
        LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
    }

    // Properties
    public ValuesClass this[object filterByFieldDefs, object filterByFieldValues]
    {
        get
        {
            ValuesClass class2 = null;
            tagVARIANT gvariant;
            tagVARIANT gvariant2;
            *((short*) &gvariant2) = 0;
            *((short*) &gvariant) = 0;
            IValues* p = null;
            IntPtr pDstNativeVariant = (IntPtr) &gvariant2;
            Marshal.GetNativeVariantForObject(filterByFieldDefs, pDstNativeVariant);
            IntPtr ptr = (IntPtr) &gvariant;
            Marshal.GetNativeVariantForObject(filterByFieldValues, ptr);
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* definitionPtr = this.m_p;
                int modopt(IsLong) hr = **(((int*) definitionPtr))[0x30](definitionPtr, gvariant2, gvariant, &p);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                IValues* valuesPtr2 = p;
                class2 = ValuesClass.CreateManagedProxy(p);
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
                VariantClear(&gvariant2);
                VariantClear(&gvariant);
                if (p != null)
                {
                    **(((int*) p))[8](p);
                }
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return class2;
        }
    }

    public bool CanSortBy
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[40](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public DatastoreItemFieldUsagesClass DatastoreItemFieldUsages
    {
        get
        {
            DatastoreItemFieldUsagesClass class2 = null;
            IDatastoreItemFieldUsages* p = null;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* definitionPtr = this.m_p;
                int modopt(IsLong) hr = **(((int*) definitionPtr))[0x34](definitionPtr, &p);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                IDatastoreItemFieldUsages* usagesPtr2 = p;
                class2 = DatastoreItemFieldUsagesClass.CreateManagedProxy(p);
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
                if (p != null)
                {
                    **(((int*) p))[8](p);
                }
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return class2;
        }
    }

    public int modopt(IsLong) DefinedSize
    {
        get
        {
            int modopt(IsLong) num2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x1c](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return num2;
        }
    }

    public string HelpText
    {
        get
        {
            string str = null;
            ushort* p = null;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* definitionPtr = this.m_p;
                int modopt(IsLong) hr = **(((int*) definitionPtr))[0x44](definitionPtr, &p);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                str = Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.ConvertResult(p);
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
                SysFreeString(p);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return str;
        }
    }

    public int modopt(IsLong) ID
    {
        get
        {
            int modopt(IsLong) num2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x10](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return num2;
        }
    }

    public uint modopt(IsLong) InternalType
    {
        get
        {
            uint modopt(IsLong) num2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x48](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return num2;
        }
    }

    public bool IsComputed
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[20](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public bool IsEditable
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x18](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public bool IsIgnored
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x2c](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public bool IsQueryable
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x24](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public bool IsReportable
    {
        [return: MarshalAs(UnmanagedType.U1)]
        get
        {
            bool flag;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                short num2;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x4c](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                flag = (num2 != 0) ? ((bool) ((byte) 1)) : ((bool) ((byte) 0));
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return flag;
        }
    }

    public string Name
    {
        get
        {
            string str = null;
            ushort* p = null;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* definitionPtr = this.m_p;
                int modopt(IsLong) hr = **(((int*) definitionPtr))[12](definitionPtr, &p);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                str = Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.ConvertResult(p);
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
                SysFreeString(p);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return str;
        }
    }

    public string ReferenceName
    {
        get
        {
            string str = null;
            ushort* p = null;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* definitionPtr = this.m_p;
                int modopt(IsLong) hr = **(((int*) definitionPtr))[0x38](definitionPtr, &p);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                str = Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.ConvertResult(p);
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
                SysFreeString(p);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return str;
        }
    }

    public int modopt(IsLong) ReportingFormula
    {
        get
        {
            int modopt(IsLong) num2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x40](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return num2;
        }
    }

    public int modopt(IsLong) ReportingType
    {
        get
        {
            int modopt(IsLong) num2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[60](p, &num2);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return num2;
        }
    }

    public PsFieldDefinitionTypeEnum Type
    {
        get
        {
            PsFieldDefinitionTypeEnum enum2;
            ErrorContext e = Context.Enter();
            CLocker* objectContextLock = Context.ObjectContextLock;
            try
            {
                PsFieldDefinitionTypeEnum enum3;
                Context.ObjectContextLock = this.m_lock;
                CLocker.Enter(this.m_lock);
                **(((int*) this.m_p))[4](this.m_p);
                IFieldDefinition* p = this.m_p;
                int modopt(IsLong) hr = **(((int*) p))[0x20](p, &enum3);
                if (hr < 0)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.DataStore.HandleComException(hr);
                }
                enum2 = (PsFieldDefinitionTypeEnum) enum3;
            }
            finally
            {
                **(((int*) this.m_p))[8](this.m_p);
                LeaveCriticalSection((_RTL_CRITICAL_SECTION*) (this.m_lock + 8));
                Context.ObjectContextLock = objectContextLock;
                Context.Exit(e);
            }
            CProdStudioObjectRoot.ReleaseRegistered();
            Context.FireEvents();
            return enum2;
        }
    }
}
}

