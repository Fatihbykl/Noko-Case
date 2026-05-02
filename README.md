- Sürüm: Unity 6000.4.0f1
- Silah: Asa

Ekranın sol üstünde can, sağ üstünde altın, sol altında upgrade menüsünü açmak için bir buton ve sağ altta da tamamen otomatik çalışan skiller var. Karakterin basic attack'ı ve skilleri tamamen otomatiktir ve menziline düşman girince otomatik çalışmaya başlar.

Skiller:
- Meteor Strike (Aktif - AoE): Her 8 saniyede bir, menzildeki düşmanların üzerine devasa bir meteor indirerek yüksek alan hasarı verir.
- Frost Aura (Pasif - Debuff): Karakterin etrafında sürekli aktif olan buzdan bir çember oluşturur. İçeri giren düşmanların hareket hızı %50 düşer.
- Arcane Overdrive (Aktif - Buff): Her 15 saniyede bir otomatik tetiklenir. 4 saniye boyunca oyuncunun Hasar (Damage) ve Saldırı Hızı (Attack Speed) istatistiklerini artırır.

Spawner:
- Spawner düşmanları otomatik olarak belirli bir alan içerisine spawnlıyor. Editörden max düşman sayısı belirlenebiliyor ve spawner düşman azaldığında toplam düşman sayısını bu sayıya tamamlıyor.

Buglar:
- Upgrade menüsünde text'lerde ve yükseltme butonlarında başlangıçta gereken altın miktarı ve current stat bilgisi yanlış gösteriyor. Upgrade butonlarına birer kere tıklayınca normale dönüyor. Bunun dışında upgrade sistemi normal çalışıyor sadece başlangıçta böyle bir text değişmeme sorunu var muhtemelen script execution order ile alakalı bir durum.
- Görsel geri bildirimler (meteor, düşman hasar alınca flash efekti gibi) hasar ile aynı anda çalışmıyor senkron sorunu var o sebeple meteor düşmeden düşmanın ölmesi gibi şeyler yaşanıyor. Bunu iyileştirmeye vakit ayıramadım.

Eksikler:
- Sadece bir düşmanı ekleyebildim diğerlerine vaktim kalmadı. Mushroom şu anda hasar alınca kaçmaya başlıyor. Stat'ındaki bir ayarla hasar alınca saldıracak şekilde düzenlenebiliyor.
- Düşmanlara can barı ve floating text
- Unity'nin default haptic biraz fazla titreştiriyor.
