#include "AmxFunction.h";
#include "amxmeta.h";
#include "common.h";
#include "exception.h";
#include "amx.h";

/** Pokemon X - sub_3D9C64 - Constructor for AMX class??? */
// Args when picking up Potion from Santalune Forest:
//      this:           0x8D3B674
//      locationIndex:  0x11E (286)    (matches Location ID)
//      funcAddr:       0x1C17 (7191)  (matches script param of NPC)
//      unk3:           0x8CEC54C      (pointer to zone data file)
AmxFunction::AmxFunction(short locationIndex, int funcAddr, char* zoneFile) {
    for (short iScript = 0; iScript < 66; iScript++) {
        if ( funcAddr < 0xEA63 ) {
            if ( funcAddr >= 1 ) {
                this->flag0            = (char) 1;
                this->funcAddr         = funcAddr;
                this->textGarcFile     = get_script_text_garc(LANG_AUTO);
                this->textGarcSubfile  = get_script_text_subfile(zoneFile);
                this->locationIndex    = locationIndex;
                this->locationMetadata = locMeta[this->locationIndex];
                this->nameIndex        = (short) -1;
                this->scriptName       = NULL;
            }
            
            return;
        }
            
        t_amxmeta0 meta0 = amxmeta0[iScript];
        
        // Functions are stored from highest index to lowest
        // in the amxmeta0 table, so we search backwards
        
        if ( funcAddr <= meta0.minFuncAddr ) {
            // Haven't found a low enough script yet
            continue;
        }
        
        if ( funcAddr > meta0.maxFuncAddr ) {
            // Invalid func address; was greater than both min and max
            // (we somehow passed the file containing the func, or it doesn't exist)
            throw_exception(); // never returns
        }

        this->amxIndex = iScript;
        this->funcAddr = funcAddr;
        this->flag0    = (char) 2;
        
        if ( meta0.textGarcFileFlag == 1 ) {
            this->textGarcFile = get_script_text_garc(LANG_AUTO);
        } else {
            this->textGarcFile = get_game_text_garc(LANG_AUTO);
        }

        this->textGarcSubfile = meta0.textGarcSubfile;
        this->locationIndex   = locationIndex;
        this->nameIndex       = meta0.nameIndex;
        this->scriptName      = AMXNAMES[meta0.nameIndex];
        
        if ( meta0.nameIndex >= 0 ) {
            this->locationMetadata = amxmeta1[meta0.nameIndex];
        } else {
            this->locationMetadata = locMeta[locationIndex];
        }
        
        return;
    }
}