﻿using System;
using LuaInterface;

namespace LuaInterfaceSample
{
    class MyLuaAPI
    {
        public Lua      Lua;

        public MyLuaAPI()
        {
            // Pass true to the constructor if you're on Mac or Linux.
            // See Lua.LoadString for an explanation
            Lua = new LuaInterface.Lua( false );
            Type self = GetType();

            // Expose some C# functions to Lua
            Lua.NewTable( "MyLuaNameSpace" );
            Lua.RegisterFunction( "MyLuaNameSpace.CoolFunction", this, self.GetMethod( "MyCoolFunction" ) );
            Lua.RegisterFunction( "MyLuaNameSpace.GetVectorLength", this, self.GetMethod( "GetVectorLength" ) );

            Lua["MyLuaNameSpace.SomeConstant" ] = 42;
        }
 
        public void MyCoolFunction( int count )
        {
            Console.WriteLine( "Got {0}!", count );
        }

        public double GetVectorLength( LuaTable _vector )
        {
            double x = (double)_vector["x"];
            double y = (double)_vector["y"];

            // Always dispose LuaTable objects after they have been used
            // This ensure the refcount on the Lua side is properly decremented
            _vector.Dispose();

            return Math.Sqrt( x*x + y*y );
        }
    }
}
