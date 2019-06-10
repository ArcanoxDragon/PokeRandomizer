#ifndef __AMX_STRUCT_101
#define __AMX_STRUCT_101

#include "AmxFunction.h";

typedef struct {
    char     str0[0x28];
    char*    str28;
} unk_000;

typedef struct {
    char     str0[0x1C];
    unk_000* obj1C;
} unk_001;

typedef struct {
    int dword0;
    int dword4;
    int dword8;
    int dwordC;
} unk_002;

class EntityBase {
public:
    EntityBase(int);
};

class Entity : public EntityBase {
protected:
    int*              vtable;
    int              dword04;
    int              dword08;
    void*              obj0C;
    int              dword10;
    int              dword14;
    int              dword18;
    short             word1A;
    AmxFunction*    function;
    int              dword28;
    int             textGarc;
    int              dword30;
    int              dword34;
    short         locationId;
    short             word3A;
    int              dword3C;
    short             word40;
    short             wordB0;
    char             byte14D;
    
public:
    Entity(int, void*, int);
};

class Entity_SubA : public Entity {
public:
    Entity_SubA(int, void*, int, int);
};

#endif