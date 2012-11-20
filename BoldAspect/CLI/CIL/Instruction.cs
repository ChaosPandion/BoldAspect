using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.CIL
{
    public sealed class Instruction
    {
        public readonly ushort Opcode;
        public readonly uint Operand;
        public readonly ushort PrefixOpcode;
        public readonly uint PrefixOperand;

        public Instruction(ushort opcode, uint operand, ushort prefixOpcode = 0, uint prefixOperand = 0)
        {
            Validate(opcode, operand, prefixOpcode, prefixOperand);
            Opcode = opcode;
            Operand = operand;
            PrefixOpcode = prefixOpcode;
            PrefixOperand = prefixOperand;
        }

        private static void Validate(ushort opcode, uint operand, ushort prefixOpcode, uint prefixOperand)
        {
            ValidatePrefix(opcode, prefixOpcode, prefixOperand);
        }

        private static void ValidatePrefix(ushort opcode, ushort prefixOpcode, uint prefixOperand)
        {
            if (prefixOpcode > 0)
            {
                switch ((int)prefixOpcode)
                {
                    case Opcodes.constrained_.Value:
                        {
                            if (opcode != Opcodes.callvirt.Value)
                                throw new CilException();
                            var token = new MetadataToken(prefixOperand);
                            switch (token.Table)
                            {
                                case TableID.TypeRef:
                                case TableID.TypeDef:
                                case TableID.TypeSpec:
                                    if (token.Key == 0)
                                        throw new CilException();
                                    break;
                                default:
                                    throw new CilException();
                            }
                        }
                        break;
                    case Opcodes.no_.Value:
                        {
                            switch (prefixOperand)
                            {
                                case Opcodes.no_.typecheck:
                                case Opcodes.no_.rangecheck:
                                case Opcodes.no_.nullcheck:
                                    break;
                                default:
                                    throw new CilException();
                            }
                        }
                        break;
                    case Opcodes.readonly_.Value:
                        {
                            if (opcode != Opcodes.ldelema.Value)
                                throw new CilException();
                        }
                        break;
                    case Opcodes.tail_.Value:
                        {
                            switch (opcode)
                            {
                                case Opcodes.call.Value:
                                case Opcodes.calli.Value:
                                case Opcodes.callvirt.Value:
                                    break;
                                default:
                                    throw new CilException();
                            }
                        }
                        break;
                    case Opcodes.unaligned_.Value:
                        {
                            switch (prefixOperand)
                            {
                                case 1:
                                case 2:
                                case 4:
                                    break;
                                default:
                                    throw new CilException();
                            }
                        }
                        break;
                    case Opcodes.volatile_.Value:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}