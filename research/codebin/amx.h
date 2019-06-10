#ifndef __AMX
#define __AMX

#define     LANG_AUTO   8

short g_game_language;
short game_text_garcs[8];
short script_text_garcs[8];

short get_game_text_garc(int langId);
short get_script_text_garc(int langId);
short get_script_text_subfile(char* zoneFile);
short get_script_unk_data(char*, int);

#endif