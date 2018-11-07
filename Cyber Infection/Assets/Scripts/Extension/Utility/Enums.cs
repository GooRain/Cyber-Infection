namespace Extension.Utility
{
 public enum CharState { Idle, Up, Down, Sideways }
 public enum BlockType { Wall, Furniture }

/*
 * 0000 = E;
 * 0001 = R;
 * 0010 = D;
 * 0011 = RD;
 * 0100 = L;
 * 0101 = LR;
 * 0110 = LD;
 * 0111 = LDR;
 * 1000 = U;
 * 1001 = UR;
 * 1010 = UD;
 * 1011 = UDR;
 * 1100 = UL;
 * 1101 = ULR;
 * 1110 = ULD;
 * 1111 = ULDR;
*/
 public enum Doors { E, R, D, RD, L, LR, LD, LDR, U, UR, UD, UDR, UL, ULR, ULD, ULDR }
}