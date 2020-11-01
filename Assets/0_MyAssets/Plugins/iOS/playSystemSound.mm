#import <Foundation/Foundation.h>
#import <AudioToolBox/AudioToolBox.h>

extern "C" void _playSystemSound (int n)
{
    AudioServicesPlaySystemSound(n);
}