using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.CIL
{
    public static class Opcodes
    {
        public static class nop
        {
            public const int Width = 1;
            public const int Value = 0x00;
        }

        public static class break_
        {
            public const int Width = 1;
            public const int Value = 0x01;
        }

        public static class ldarg_0
        {
            public const int Width = 1;
            public const int Value = 0x02;
        }

        public static class ldarg_1
        {
            public const int Width = 1;
            public const int Value = 0x03;
        }

        public static class ldarg_2
        {
            public const int Width = 1;
            public const int Value = 0x04;
        }

        public static class ldarg_3
        {
            public const int Width = 1;
            public const int Value = 0x05;
        }

        public static class ldloc_0
        {
            public const int Width = 1;
            public const int Value = 0x06;
        }

        public static class ldloc_1
        {
            public const int Width = 1;
            public const int Value = 0x07;
        }

        public static class ldloc_2
        {
            public const int Width = 1;
            public const int Value = 0x08;
        }

        public static class ldloc_3
        {
            public const int Width = 1;
            public const int Value = 0x09;
        }

        public static class stloc_0
        {
            public const int Width = 1;
            public const int Value = 0x0A;
        }

        public static class stloc_1
        {
            public const int Width = 1;
            public const int Value = 0x0B;
        }

        public static class stloc_2
        {
            public const int Width = 1;
            public const int Value = 0x0C;
        }

        public static class stloc_3
        {
            public const int Width = 1;
            public const int Value = 0x0D;
        }

        public static class ldarg_s
        {
            public const int Width = 1;
            public const int Value = 0x0E;
        }

        public static class ldarga_s
        {
            public const int Width = 1;
            public const int Value = 0x0F;
        }

        public static class starg_s
        {
            public const int Width = 1;
            public const int Value = 0x10;
        }

        public static class ldloc_s
        {
            public const int Width = 1;
            public const int Value = 0x11;
        }

        public static class ldloca_s
        {
            public const int Width = 1;
            public const int Value = 0x12;
        }

        public static class stloc_s
        {
            public const int Width = 1;
            public const int Value = 0x13;
        }

        public static class ldnull
        {
            public const int Width = 1;
            public const int Value = 0x14;
        }

        public static class ldc_i4_m1
        {
            public const int Width = 1;
            public const int Value = 0x15;
        }

        public static class ldc_i4_0
        {
            public const int Width = 1;
            public const int Value = 0x16;
        }

        public static class ldc_i4_1
        {
            public const int Width = 1;
            public const int Value = 0x17;
        }

        public static class ldc_i4_2
        {
            public const int Width = 1;
            public const int Value = 0x18;
        }

        public static class ldc_i4_3
        {
            public const int Width = 1;
            public const int Value = 0x19;
        }

        public static class ldc_i4_4
        {
            public const int Width = 1;
            public const int Value = 0x1A;
        }

        public static class ldc_i4_5
        {
            public const int Width = 1;
            public const int Value = 0x1B;
        }

        public static class ldc_i4_6
        {
            public const int Width = 1;
            public const int Value = 0x1C;
        }

        public static class ldc_i4_7
        {
            public const int Width = 1;
            public const int Value = 0x1D;
        }

        public static class ldc_i4_8
        {
            public const int Width = 1;
            public const int Value = 0x1E;
        }

        public static class ldc_i4_s
        {
            public const int Width = 1;
            public const int Value = 0x1F;
        }

        public static class ldc_i4
        {
            public const int Width = 1;
            public const int Value = 0x20;
        }

        public static class ldc_i8
        {
            public const int Width = 1;
            public const int Value = 0x21;
        }

        public static class ldc_r4
        {
            public const int Width = 1;
            public const int Value = 0x22;
        }

        public static class ldc_r8
        {
            public const int Width = 1;
            public const int Value = 0x23;
        }

        public static class dup
        {
            public const int Width = 1;
            public const int Value = 0x25;
        }

        public static class pop
        {
            public const int Width = 1;
            public const int Value = 0x26;
        }

        public static class jmp
        {
            public const int Width = 1;
            public const int Value = 0x27;
        }

        public static class call
        {
            public const int Width = 1;
            public const int Value = 0x28;
        }

        public static class calli
        {
            public const int Width = 1;
            public const int Value = 0x29;
        }

        public static class ret
        {
            public const int Width = 1;
            public const int Value = 0x2A;
        }

        public static class br_s
        {
            public const int Width = 1;
            public const int Value = 0x2B;
        }

        public static class brfalse_s
        {
            public const int Width = 1;
            public const int Value = 0x2C;
        }

        public static class brtrue_s
        {
            public const int Width = 1;
            public const int Value = 0x2D;
        }

        public static class beq_s
        {
            public const int Width = 1;
            public const int Value = 0x2E;
        }

        public static class bge_s
        {
            public const int Width = 1;
            public const int Value = 0x2F;
        }

        public static class bgt_s
        {
            public const int Width = 1;
            public const int Value = 0x30;
        }

        public static class ble_s
        {
            public const int Width = 1;
            public const int Value = 0x31;
        }

        public static class blt_s
        {
            public const int Width = 1;
            public const int Value = 0x32;
        }

        public static class bne_un_s
        {
            public const int Width = 1;
            public const int Value = 0x33;
        }

        public static class bge_un_s
        {
            public const int Width = 1;
            public const int Value = 0x34;
        }

        public static class bgt_un_s
        {
            public const int Width = 1;
            public const int Value = 0x35;
        }

        public static class ble_un_s
        {
            public const int Width = 1;
            public const int Value = 0x36;
        }

        public static class blt_un_s
        {
            public const int Width = 1;
            public const int Value = 0x37;
        }

        public static class br
        {
            public const int Width = 1;
            public const int Value = 0x38;
        }

        public static class brfalse
        {
            public const int Width = 1;
            public const int Value = 0x39;
        }

        public static class brtrue
        {
            public const int Width = 1;
            public const int Value = 0x3A;
        }

        public static class beq
        {
            public const int Width = 1;
            public const int Value = 0x3B;
        }

        public static class bge
        {
            public const int Width = 1;
            public const int Value = 0x3C;
        }

        public static class bgt
        {
            public const int Width = 1;
            public const int Value = 0x3D;
        }

        public static class ble
        {
            public const int Width = 1;
            public const int Value = 0x3E;
        }

        public static class blt
        {
            public const int Width = 1;
            public const int Value = 0x3F;
        }

        public static class bne_un
        {
            public const int Width = 1;
            public const int Value = 0x40;
        }

        public static class bge_un
        {
            public const int Width = 1;
            public const int Value = 0x41;
        }

        public static class bgt_un
        {
            public const int Width = 1;
            public const int Value = 0x42;
        }

        public static class ble_un
        {
            public const int Width = 1;
            public const int Value = 0x43;
        }

        public static class blt_un
        {
            public const int Width = 1;
            public const int Value = 0x44;
        }

        public static class switch_
        {
            public const int Width = 1;
            public const int Value = 0x45;
        }

        public static class ldind_i1
        {
            public const int Width = 1;
            public const int Value = 0x46;
        }

        public static class ldind_u1
        {
            public const int Width = 1;
            public const int Value = 0x47;
        }

        public static class ldind_i2
        {
            public const int Width = 1;
            public const int Value = 0x48;
        }

        public static class ldind_u2
        {
            public const int Width = 1;
            public const int Value = 0x49;
        }

        public static class ldind_i4
        {
            public const int Width = 1;
            public const int Value = 0x4A;
        }

        public static class ldind_u4
        {
            public const int Width = 1;
            public const int Value = 0x4B;
        }

        public static class ldind_i8
        {
            public const int Width = 1;
            public const int Value = 0x4C;
        }

        public static class ldind_i
        {
            public const int Width = 1;
            public const int Value = 0x4D;
        }

        public static class ldind_r4
        {
            public const int Width = 1;
            public const int Value = 0x4E;
        }

        public static class ldind_r8
        {
            public const int Width = 1;
            public const int Value = 0x4F;
        }

        public static class ldind_ref
        {
            public const int Width = 1;
            public const int Value = 0x50;
        }

        public static class stind_ref
        {
            public const int Width = 1;
            public const int Value = 0x51;
        }

        public static class stind_i1
        {
            public const int Width = 1;
            public const int Value = 0x52;
        }

        public static class stind_i2
        {
            public const int Width = 1;
            public const int Value = 0x53;
        }

        public static class stind_i4
        {
            public const int Width = 1;
            public const int Value = 0x54;
        }

        public static class stind_i8
        {
            public const int Width = 1;
            public const int Value = 0x55;
        }

        public static class stind_r4
        {
            public const int Width = 1;
            public const int Value = 0x56;
        }

        public static class stind_r8
        {
            public const int Width = 1;
            public const int Value = 0x57;
        }

        public static class add
        {
            public const int Width = 1;
            public const int Value = 0x58;
        }

        public static class sub
        {
            public const int Width = 1;
            public const int Value = 0x59;
        }

        public static class mul
        {
            public const int Width = 1;
            public const int Value = 0x5A;
        }

        public static class div
        {
            public const int Width = 1;
            public const int Value = 0x5B;
        }

        public static class div_un
        {
            public const int Width = 1;
            public const int Value = 0x5C;
        }

        public static class rem
        {
            public const int Width = 1;
            public const int Value = 0x5D;
        }

        public static class rem_un
        {
            public const int Width = 1;
            public const int Value = 0x5E;
        }

        public static class and
        {
            public const int Width = 1;
            public const int Value = 0x5F;
        }

        public static class or
        {
            public const int Width = 1;
            public const int Value = 0x60;
        }

        public static class xor
        {
            public const int Width = 1;
            public const int Value = 0x61;
        }

        public static class shl
        {
            public const int Width = 1;
            public const int Value = 0x62;
        }

        public static class shr
        {
            public const int Width = 1;
            public const int Value = 0x63;
        }

        public static class shr_un
        {
            public const int Width = 1;
            public const int Value = 0x64;
        }

        public static class neg
        {
            public const int Width = 1;
            public const int Value = 0x65;
        }

        public static class not
        {
            public const int Width = 1;
            public const int Value = 0x66;
        }

        public static class conv_i1
        {
            public const int Width = 1;
            public const int Value = 0x67;
        }

        public static class conv_i2
        {
            public const int Width = 1;
            public const int Value = 0x68;
        }

        public static class conv_i4
        {
            public const int Width = 1;
            public const int Value = 0x69;
        }

        public static class conv_i8
        {
            public const int Width = 1;
            public const int Value = 0x6A;
        }

        public static class conv_r4
        {
            public const int Width = 1;
            public const int Value = 0x6B;
        }

        public static class conv_r8
        {
            public const int Width = 1;
            public const int Value = 0x6C;
        }

        public static class conv_u4
        {
            public const int Width = 1;
            public const int Value = 0x6D;
        }

        public static class conv_u8
        {
            public const int Width = 1;
            public const int Value = 0x6E;
        }

        public static class callvirt
        {
            public const int Width = 1;
            public const int Value = 0x6F;
        }

        public static class cpobj
        {
            public const int Width = 1;
            public const int Value = 0x70;
        }

        public static class ldobj
        {
            public const int Width = 1;
            public const int Value = 0x71;
        }

        public static class ldstr
        {
            public const int Width = 1;
            public const int Value = 0x72;
        }

        public static class newobj
        {
            public const int Width = 1;
            public const int Value = 0x73;
        }

        public static class castclass
        {
            public const int Width = 1;
            public const int Value = 0x74;
        }

        public static class isinst
        {
            public const int Width = 1;
            public const int Value = 0x75;
        }

        public static class conv_r_un
        {
            public const int Width = 1;
            public const int Value = 0x76;
        }

        public static class unbox
        {
            public const int Width = 1;
            public const int Value = 0x79;
        }

        public static class throw_
        {
            public const int Width = 1;
            public const int Value = 0x7A;
        }

        public static class ldfld
        {
            public const int Width = 1;
            public const int Value = 0x7B;
        }

        public static class ldflda
        {
            public const int Width = 1;
            public const int Value = 0x7C;
        }

        public static class stfld
        {
            public const int Width = 1;
            public const int Value = 0x7D;
        }

        public static class ldsfld
        {
            public const int Width = 1;
            public const int Value = 0x7E;
        }

        public static class ldsflda
        {
            public const int Width = 1;
            public const int Value = 0x7F;
        }

        public static class stsfld
        {
            public const int Width = 1;
            public const int Value = 0x80;
        }

        public static class stobj
        {
            public const int Width = 1;
            public const int Value = 0x81;
        }

        public static class conv_ovf_i1_un
        {
            public const int Width = 1;
            public const int Value = 0x82;
        }

        public static class conv_ovf_i2_un
        {
            public const int Width = 1;
            public const int Value = 0x83;
        }

        public static class conv_ovf_i4_un
        {
            public const int Width = 1;
            public const int Value = 0x84;
        }

        public static class conv_ovf_i8_un
        {
            public const int Width = 1;
            public const int Value = 0x85;
        }

        public static class conv_ovf_u1_un
        {
            public const int Width = 1;
            public const int Value = 0x86;
        }

        public static class conv_ovf_u2_un
        {
            public const int Width = 1;
            public const int Value = 0x87;
        }

        public static class conv_ovf_u4_un
        {
            public const int Width = 1;
            public const int Value = 0x88;
        }

        public static class conv_ovf_u8_un
        {
            public const int Width = 1;
            public const int Value = 0x89;
        }

        public static class conv_ovf_i_un
        {
            public const int Width = 1;
            public const int Value = 0x8A;
        }

        public static class conv_ovf_u_un
        {
            public const int Width = 1;
            public const int Value = 0x8B;
        }

        public static class box
        {
            public const int Width = 1;
            public const int Value = 0x8C;
        }

        public static class newarr
        {
            public const int Width = 1;
            public const int Value = 0x8D;
        }

        public static class ldlen
        {
            public const int Width = 1;
            public const int Value = 0x8E;
        }

        public static class ldelema
        {
            public const int Width = 1;
            public const int Value = 0x8F;
        }

        public static class ldelem_i1
        {
            public const int Width = 1;
            public const int Value = 0x90;
        }

        public static class ldelem_u1
        {
            public const int Width = 1;
            public const int Value = 0x91;
        }

        public static class ldelem_i2
        {
            public const int Width = 1;
            public const int Value = 0x92;
        }

        public static class ldelem_u2
        {
            public const int Width = 1;
            public const int Value = 0x93;
        }

        public static class ldelem_i4
        {
            public const int Width = 1;
            public const int Value = 0x94;
        }

        public static class ldelem_u4
        {
            public const int Width = 1;
            public const int Value = 0x95;
        }

        public static class ldelem_i8
        {
            public const int Width = 1;
            public const int Value = 0x96;
        }

        public static class ldelem_i
        {
            public const int Width = 1;
            public const int Value = 0x97;
        }

        public static class ldelem_r4
        {
            public const int Width = 1;
            public const int Value = 0x98;
        }

        public static class ldelem_r8
        {
            public const int Width = 1;
            public const int Value = 0x99;
        }

        public static class ldelem_ref
        {
            public const int Width = 1;
            public const int Value = 0x9A;
        }

        public static class stelem_i
        {
            public const int Width = 1;
            public const int Value = 0x9B;
        }

        public static class stelem_i1
        {
            public const int Width = 1;
            public const int Value = 0x9C;
        }

        public static class stelem_i2
        {
            public const int Width = 1;
            public const int Value = 0x9D;
        }

        public static class stelem_i4
        {
            public const int Width = 1;
            public const int Value = 0x9E;
        }

        public static class stelem_i8
        {
            public const int Width = 1;
            public const int Value = 0x9F;
        }

        public static class stelem_r4
        {
            public const int Width = 1;
            public const int Value = 0xA0;
        }

        public static class stelem_r8
        {
            public const int Width = 1;
            public const int Value = 0xA1;
        }

        public static class stelem_ref
        {
            public const int Width = 1;
            public const int Value = 0xA2;
        }

        public static class ldelem
        {
            public const int Width = 1;
            public const int Value = 0xA3;
        }

        public static class stelem
        {
            public const int Width = 1;
            public const int Value = 0xA4;
        }

        public static class unbox_any
        {
            public const int Width = 1;
            public const int Value = 0xA5;
        }

        public static class conv_ovf_i1
        {
            public const int Width = 1;
            public const int Value = 0xB3;
        }

        public static class conv_ovf_u1
        {
            public const int Width = 1;
            public const int Value = 0xB4;
        }

        public static class conv_ovf_i2
        {
            public const int Width = 1;
            public const int Value = 0xB5;
        }

        public static class conv_ovf_u2
        {
            public const int Width = 1;
            public const int Value = 0xB6;
        }

        public static class conv_ovf_i4
        {
            public const int Width = 1;
            public const int Value = 0xB7;
        }

        public static class conv_ovf_u4
        {
            public const int Width = 1;
            public const int Value = 0xB8;
        }

        public static class conv_ovf_i8
        {
            public const int Width = 1;
            public const int Value = 0xB9;
        }

        public static class conv_ovf_u8
        {
            public const int Width = 1;
            public const int Value = 0xBA;
        }

        public static class refanyval
        {
            public const int Width = 1;
            public const int Value = 0xC2;
        }

        public static class ckfinite
        {
            public const int Width = 1;
            public const int Value = 0xC3;
        }

        public static class mkrefany
        {
            public const int Width = 1;
            public const int Value = 0xC6;
        }

        public static class ldtoken
        {
            public const int Width = 1;
            public const int Value = 0xD0;
        }

        public static class conv_u2
        {
            public const int Width = 1;
            public const int Value = 0xD1;
        }

        public static class conv_u1
        {
            public const int Width = 1;
            public const int Value = 0xD2;
        }

        public static class conv_i
        {
            public const int Width = 1;
            public const int Value = 0xD3;
        }

        public static class conv_ovf_i
        {
            public const int Width = 1;
            public const int Value = 0xD4;
        }

        public static class conv_ovf_u
        {
            public const int Width = 1;
            public const int Value = 0xD5;
        }

        public static class add_ovf
        {
            public const int Width = 1;
            public const int Value = 0xD6;
        }

        public static class add_ovf_un
        {
            public const int Width = 1;
            public const int Value = 0xD7;
        }

        public static class mul_ovf
        {
            public const int Width = 1;
            public const int Value = 0xD8;
        }

        public static class mul_ovf_un
        {
            public const int Width = 1;
            public const int Value = 0xD9;
        }

        public static class sub_ovf
        {
            public const int Width = 1;
            public const int Value = 0xDA;
        }

        public static class sub_ovf_un
        {
            public const int Width = 1;
            public const int Value = 0xDB;
        }

        public static class endfinally
        {
            public const int Width = 1;
            public const int Value = 0xDC;
        }

        public static class leave
        {
            public const int Width = 1;
            public const int Value = 0xDD;
        }

        public static class leave_s
        {
            public const int Width = 1;
            public const int Value = 0xDE;
        }

        public static class stind_i
        {
            public const int Width = 1;
            public const int Value = 0xDF;
        }

        public static class conv_u
        {
            public const int Width = 1;
            public const int Value = 0xE0;
        }

        public static class arglist
        {
            public const int Width = 2;
            public const int Value = 0xFE00;
        }

        public static class ceq
        {
            public const int Width = 2;
            public const int Value = 0xFE01;
        }

        public static class cgt
        {
            public const int Width = 2;
            public const int Value = 0xFE02;
        }

        public static class cgt_un
        {
            public const int Width = 2;
            public const int Value = 0xFE03;
        }

        public static class clt
        {
            public const int Width = 2;
            public const int Value = 0xFE04;
        }

        public static class clt_un
        {
            public const int Width = 2;
            public const int Value = 0xFE05;
        }

        public static class ldftn
        {
            public const int Width = 2;
            public const int Value = 0xFE06;
        }

        public static class ldvirtftn
        {
            public const int Width = 2;
            public const int Value = 0xFE07;
        }

        public static class ldarg
        {
            public const int Width = 2;
            public const int Value = 0xFE09;
        }

        public static class ldarga
        {
            public const int Width = 2;
            public const int Value = 0xFE0A;
        }

        public static class starg
        {
            public const int Width = 2;
            public const int Value = 0xFE0B;
        }

        public static class ldloc
        {
            public const int Width = 2;
            public const int Value = 0xFE0C;
        }

        public static class ldloca
        {
            public const int Width = 2;
            public const int Value = 0xFE0D;
        }

        public static class stloc
        {
            public const int Width = 2;
            public const int Value = 0xFE0E;
        }

        public static class localloc
        {
            public const int Width = 2;
            public const int Value = 0xFE0F;
        }

        public static class endfilter
        {
            public const int Width = 2;
            public const int Value = 0xFE11;
        }

        public static class unaligned_
        {
            public const int Width = 2;
            public const int Value = 0xFE12;
        }

        public static class volatile_
        {
            public const int Width = 2;
            public const int Value = 0xFE13;
        }

        public static class tail_
        {
            public const int Width = 2;
            public const int Value = 0xFE14;
        }

        public static class Initobj
        {
            public const int Width = 2;
            public const int Value = 0xFE15;
        }

        public static class constrained_
        {
            public const int Width = 2;
            public const int Value = 0xFE16;
        }

        public static class cpblk
        {
            public const int Width = 2;
            public const int Value = 0xFE17;
        }

        public static class initblk
        {
            public const int Width = 2;
            public const int Value = 0xFE18;
        }

        public static class no_
        {
            public const int Width = 2;
            public const int Value = 0xFE19;
            public const int typecheck = 1;
            public const int rangecheck = 2;
            public const int nullcheck = 3;
        }

        public static class rethrow
        {
            public const int Width = 2;
            public const int Value = 0xFE1A;
        }

        public static class sizeof_
        {
            public const int Width = 2;
            public const int Value = 0xFE1C;
        }

        public static class Refanytype
        {
            public const int Width = 2;
            public const int Value = 0xFE1D;
        }

        public static class readonly_
        {
            public const int Width = 2;
            public const int Value = 0xFE1E;
        }
    }
}