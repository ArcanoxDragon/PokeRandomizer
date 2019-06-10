#include "common.h"
#include "amx.h";
#include "entity.h"
#include "AmxFunction.h"

int sub_1228EC(int, void*);
void sub_15E914(short, int);

Entity::Entity(int a2, void* a3, int a5) 
    : EntityBase(a2) {
    this->dword08  = a2;
    this->textGarc = get_script_text_garc(LANG_AUTO);
    this->word3A   = -1;
    this->dword14  = sub_1228EC(a5 * 4, a3);
    this->dword18  = a5;
    
    sub_15E914(this->dword14, a5 * 4);
    
    this->wordB0   = 240;
}

Entity_SubA::Entity_SubA(int a2, void* a3, int funcAddr, int a5)
    : Entity(a2, a3, a5) {
    if (funcAddr != -1) {
        this->function = new AmxFunction(this->locationId, funcAddr, this->obj0C->obj1C->pchar28);
    }
}