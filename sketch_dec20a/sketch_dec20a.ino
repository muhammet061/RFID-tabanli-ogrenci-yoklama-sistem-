#include <SPI.h>
#include <MFRC522.h> // RFID kütüphanesi

#define RST_PIN 9   // RFID için RST pini
#define SS_PIN 10   // RFID için SDA (SS) pini

// Motor pinleri
#define IN1 6
#define IN2 7
#define IN3 4
#define IN4 5


MFRC522 mfrc522(SS_PIN, RST_PIN); // RFID modülü için nesne

int kartSayisi = 0;                  // Okunan kart sayacı
unsigned long sonKartZamani = 0;     // Son kart okuma zamanı
const unsigned long debounceSuresi = 1000; // Kart okuma aralığı (ms)

bool motorCalisiyor = false;         // Motorların çalışma durumu
unsigned long motorBaslangicZamani;  // Motor çalışma başlangıç zamanı
const unsigned long motorCalismaSuresi = 10000; // Motorların çalışma süresi (ms)

void setup() {
  Serial.begin(9600);    // Seri iletişim başlat
  SPI.begin();           // SPI iletişimi başlat
  mfrc522.PCD_Init();    // RFID modülü başlat

  // Motor pinlerini çıkış olarak ayarla
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);


  // Motorlar başlangıçta kapalı
  motorlariKapat();
}

void loop() {
  if (motorCalisiyor) {
    motorKontrol(); // Motor çalışma süresini kontrol et
  } else {
    kartOkuma(); // Kart okuma işlemini devam ettir
  }
}

// Kart okuma işlemi
void kartOkuma() {
  // Kart algılanmadıysa devam et
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  // Kart verisi okunamadıysa devam et
  if (!mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  // Aynı kartın kısa sürede tekrar okunmasını engelle
  unsigned long simdikiZaman = millis();
  if (simdikiZaman - sonKartZamani < debounceSuresi) {
    return;
  }
  sonKartZamani = simdikiZaman; // Son kart okuma zamanını güncelle

  // Kart UID'sini oku ve yazdır
  String cardUID = "";
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    cardUID += String(mfrc522.uid.uidByte[i], HEX);
    if (i < mfrc522.uid.size - 1) cardUID += " ";
  }
  cardUID.toUpperCase(); // UID'yi büyük harfe çevir
  Serial.println("Kart UID: " + cardUID);

  // Kart sayısını artır
  kartSayisi++;

  // 5 karta ulaşıldığında motorları çalıştır
  if (kartSayisi == 7) {
    motorCalisiyor = true;
    motorBaslangicZamani = millis();
    motorlariCalistir();
    kartSayisi = 0; // Sayaç sıfırlanır
  }
}

// Motor çalışma süresi kontrolü
void motorKontrol() {
  unsigned long simdikiZaman = millis();
  if (simdikiZaman - motorBaslangicZamani >= motorCalismaSuresi) {
    motorlariKapat(); // Tüm motorları durdur
    motorCalisiyor = false; // Motor çalışma durumu sona erer
  }
}

// Tüm motorları aynı anda çalıştıran fonksiyon
void motorlariCalistir() {
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);

  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);



}

// Tüm motorları kapatan fonksiyon
void motorlariKapat() {
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, LOW);

  digitalWrite(IN3, LOW);
  digitalWrite(IN4, LOW);



}





