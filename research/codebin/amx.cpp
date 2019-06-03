#include "amx.h";
#include "exception.h";

short get_game_text_garc(int language_id) {
    if (language_id == 8 && g_game_language >= 8) {
        throw_exception(); // never returns
    }
    
    return game_text_garcs[language_id];
}

short get_script_text_garc(int language_id) {
    if (language_id == 8) {
        language_id = g_game_language;
    }
    
    return script_text_garcs[language_id];
}