# 🎮 五子棋 (Gomoku) - Windows Programming 

這是一個使用 C# Windows Forms (WinForms) 與 GDI+ 繪圖技術開發的五子棋雙人對戰遊戲。此專案為「視窗程式設計 (II)」課程之棋牌類遊戲作業。

## ✨ 遊戲特色 (Features)
* **純程式碼動態繪製 (GDI+)**：不依賴任何拖拉的 UI 圖片元件，棋盤、格線、星位與立體感棋子皆由程式碼即時運算繪製。
* **精準的落子判定**：透過座標轉換演算法，確保玩家點擊時，棋子能完美吸附並置中於交叉點。
* **智慧勝負判定**：實作四方向 (水平、垂直、左上右下、右上左下) 連線演算法，即時偵測五子連線。
* **客製化沉浸式 UI**：
  * 遊戲結束時彈出帶有史詩感 (League of Legends VICTORY) 背景圖的自訂結算視窗。
  * 支援透明背景字體與無邊框焦點按鈕設計。
* **音效支援**：整合 `SoundPlayer`，支援落子音效與獲勝音效。

## 🚀 執行說明 (How to Run)
1. 確保您的電腦已安裝 Windows 作業系統與 Visual Studio (支援 .NET Framework)。
2. 將本儲存庫 (Repository) Clone 或下載至本地端。
3. 雙擊開啟方案檔 (`.sln`)。
4. 按下 `F5` 或點擊「開始」進行編譯與執行。
5. *備註：本專案已設定 `.gitignore`，因此倉庫內不含 `bin/` 與 `obj/` 暫存檔。編譯時系統會自動產生所需的執行環境。*

## 📸 遊戲畫面截圖 (Screenshots)


<img width="869" height="527" alt="image" src="https://github.com/user-attachments/assets/dc2957a2-1d51-4cd2-8a9c-f8d406912359" />

15x15 標準五子棋盤，帶有星位與天元標示，棋子具備立體反光效果。
客製化結算視窗，帶有金色勝利提示字樣以及重新開始/離開按鈕。

## 🛠️ 技術架構 (Technologies)
* **語言**: C#
* **框架**: .NET Framework (Windows Forms)
* **繪圖**: System.Drawing (GDI+)
* **音效**: System.Media.SoundPlayer
