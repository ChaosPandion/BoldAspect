﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BoldAspect.CLI;

namespace BoldAspect.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseOpcodes();
        }

        static void Build()
        {
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            var module = CLIModule.Load(fileName);
        }

        static void ParseOpcodes()
        {
            var sb = new StringBuilder();
            var matches = Regex.Matches(_opcodes, @"(?:(0xFE\s+0x(..))|(0x..))\s*(.*)");
            foreach (var match in matches.Cast<Match>())
            {
                var name = match.Groups[4].Value.Replace('.', '_');
                sb.AppendFormat("public static class {0}\r\n{{\r\n", name);

                var width = 1;
                string value = null;
                if (match.Groups[1].Success)
                {
                    value = "0xFE" + match.Groups[2].Value;
                    width = 2;
                }
                else
                {
                    value = match.Groups[3].Value;
                }
                sb.AppendFormat("public const int Width = {0};\r\n", width);
                sb.AppendFormat("public const int Value = {0};\r\n", value);
                sb.Append("}\r\n\r\n");
            }
            Debug.Write(sb);
        }


        private static readonly string _opcodes = @"0x00  nop 
0x01  break 
0x02  ldarg.0 
0x03  ldarg.1 
0x04  ldarg.2 
0x05  ldarg.3 
0x06  ldloc.0 
0x07  ldloc.1 
0x08  ldloc.2 
0x09  ldloc.3 
0x0A  stloc.0 
0x0B  stloc.1 
0x0C  stloc.2 
0x0D  stloc.3 
0x0E  ldarg.s 
0x0F  ldarga.s 
0x10  starg.s 
0x11  ldloc.s 
0x12  ldloca.s 
0x13  stloc.s 
0x14  ldnull 
0x15  ldc.i4.m1 
0x16  ldc.i4.0 
0x17  ldc.i4.1 
0x18  ldc.i4.2 
0x19  ldc.i4.3 
0x1A  ldc.i4.4 
0x1B  ldc.i4.5 
0x1C  ldc.i4.6 
0x1D  ldc.i4.7 
0x1E  ldc.i4.8 
0x1F  ldc.i4.s 
0x20  ldc.i4 
0x21  ldc.i8 
0x22  ldc.r4 
0x23  ldc.r8 
0x25  dup 
0x26  pop 
0x27  jmp 
0x28  call 
0x29  calli 
0x2A  ret 
0x2B  br.s 
0x2C  brfalse.s 
0x2D  brtrue.s 
0x2E  beq.s 
0x2F  bge.s 
0x30  bgt.s 
0x31  ble.s 
0x32  blt.s 
0x33  bne.un.s 
0x34  bge.un.s 
0x35  bgt.un.s 
0x36  ble.un.s 
0x37  blt.un.s 
0x38  br 
0x39  brfalse 
0x3A  brtrue 
0x3B  beq 
0x3C  bge 
0x3D  bgt 
0x3E  ble 
0x3F  blt 
0x40  bne.un 
0x41  bge.un 
0x42  bgt.un 
0x43  ble.un 
0x44  blt.un 
0x45  switch 
0x46  ldind.i1 
0x47  ldind.u1 
0x48  ldind.i2 
0x49  ldind.u2 
0x4A  ldind.i4 
0x4B  ldind.u4 
0x4C  ldind.i8 
0x4D  ldind.i 
0x4E  ldind.r4 
0x4F  ldind.r8 
0x50  ldind.ref 
0x51  stind.ref 
0x52  stind.i1 
0x53  stind.i2 
0x54  stind.i4 
0x55  stind.i8 
0x56  stind.r4 
0x57  stind.r8 
0x58  add 
0x59  sub 
0x5A  mul 
0x5B  div 
0x5C  div.un 
0x5D  rem 
0x5E  rem.un 
0x5F  and 
0x60  or 
0x61  xor 
0x62  shl 
0x63  shr 
0x64  shr.un 
0x65  neg 
0x66  not 
0x67  conv.i1 
0x68  conv.i2 
0x69  conv.i4 
0x6A  conv.i8 
0x6B  conv.r4 
0x6C  conv.r8 
0x6D  conv.u4 
0x6E  conv.u8 
0x6F  callvirt 
0x70  cpobj 
0x71  ldobj 
0x72  ldstr 
0x73  newobj 
0x74  castclass 
0x75  isinst 
0x76  conv.r.un 
0x79  unbox 
0x7A  throw 
0x7B  ldfld 
0x7C  ldflda 
0x7D  stfld 
0x7E  ldsfld 
0x7F  ldsflda 
0x80  stsfld 
0x81  stobj 
0x82  conv.ovf.i1.un 
0x83  conv.ovf.i2.un 
0x84  conv.ovf.i4.un 
0x85  conv.ovf.i8.un 
0x86  conv.ovf.u1.un 
0x87  conv.ovf.u2.un 
0x88  conv.ovf.u4.un 
0x89  conv.ovf.u8.un 
0x8A  conv.ovf.i.un 
0x8B  conv.ovf.u.un 
0x8C  box 
0x8D  newarr 
0x8E  ldlen 
0x8F  ldelema 
0x90  ldelem.i1 
0x91  ldelem.u1 
0x92  ldelem.i2 
0x93  ldelem.u2 
0x94  ldelem.i4 
0x95  ldelem.u4 
0x96  ldelem.i8 
0x97  ldelem.i 
0x98  ldelem.r4 
0x99  ldelem.r8 
0x9A  ldelem.ref 
0x9B  stelem.i 
0x9C  stelem.i1 
0x9D  stelem.i2 
0x9E  stelem.i4 
0x9F  stelem.i8 
0xA0  stelem.r4 
0xA1  stelem.r8 
0xA2  stelem.ref 
0xA3  ldelem 
0xA4  stelem 
0xA5  unbox.any 
0xB3  conv.ovf.i1 
0xB4  conv.ovf.u1 
0xB5  conv.ovf.i2 
0xB6  conv.ovf.u2 
0xB7  conv.ovf.i4 
0xB8  conv.ovf.u4 
0xB9  conv.ovf.i8 
0xBA  conv.ovf.u8 
0xC2  refanyval 
0xC3  ckfinite 
0xC6  mkrefany 
0xD0  ldtoken 
0xD1  conv.u2 
0xD2  conv.u1 
0xD3  conv.i 
0xD4  conv.ovf.i 
0xD5  conv.ovf.u 
0xD6  add.ovf 
0xD7  add.ovf.un 
0xD8  mul.ovf 
0xD9  mul.ovf.un 
0xDA  sub.ovf 
0xDB  sub.ovf.un 
0xDC  endfinally 
0xDD  leave 
0xDE  leave.s 
0xDF  stind.i 
0xE0  conv.u 
0xFE 0x00  arglist 
0xFE 0x01  ceq 
0xFE 0x02  cgt 
0xFE 0x03  cgt.un 
0xFE 0x04  clt 
0xFE 0x05  clt.un 
0xFE 0x06  ldftn 
0xFE 0x07  ldvirtftn 
0xFE 0x09  ldarg 
0xFE 0x0A  ldarga 
0xFE 0x0B  starg 
0xFE 0x0C  ldloc 
0xFE 0x0D  ldloca 
0xFE 0x0E  stloc 
0xFE 0x0F  localloc 
0xFE 0x11  endfilter 
0xFE 0x12  unaligned. 
0xFE 0x13  volatile. 
0xFE 0x14  tail. 
0xFE 0x15  Initobj 
0xFE 0x16  constrained. 
0xFE 0x17  cpblk 
0xFE 0x18  initblk 
0xFE 0x19  no. 
0xFE 0x1A  rethrow 
0xFE 0x1C  sizeof 
0xFE 0x1D  Refanytype 
0xFE 0x1E  readonly.";
    }
}