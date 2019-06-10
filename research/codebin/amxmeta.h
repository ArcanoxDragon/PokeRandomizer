#ifndef __AMX_META
#define __AMX_META

typedef struct {
    short   minFuncAddr;
    short   maxFuncAddr;
    short   nameIndex;
    short   textGarcFileFlag; // always 1?
    short   textGarcSubfile;
    short   word_0A;
    short   word_0C;
    short   word_0E;
} t_amxmeta0;

t_amxmeta0 amxmeta0[66];
long  amxmeta1[];
long  locMeta[];
char* AMXNAMES[];

#endif