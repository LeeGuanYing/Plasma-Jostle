Plasma-Jostle Input System 修正 Patch
========================================

目標 Unity Editor:
6000.6.0f1

修改:
- 新增 Packages/manifest.json
- 加入 com.unity.inputsystem 1.20.0
- 加入 com.unity.render-pipelines.universal 17.6.0

套用方式:
1. 關閉 Unity Editor。
2. 將本 ZIP 內的 Packages/manifest.json 解壓縮/複製到 Plasma-Jostle 專案根目錄。
3. 如果本機已經存在 Packages/manifest.json，請不要直接覆蓋；先保留原檔並確認其中其他 dependencies，再把上述兩項加入 dependencies。
4. 重新用 Unity 6000.6.0f1 開啟專案。
5. 等待 Package Manager 完成解析與 Asset import。
6. 檢查 Console，確認 UnityEngine.InputSystem 的 CS0234/CS0246 消失。

注意:
- 本 Patch 不修改任何 C# 原始碼。
- 不包含 Library/、Temp/ 或 packages-lock.json；packages-lock.json 應由 Unity Package Manager 依 manifest 重新產生。
- 如果套用後出現新的 Package Manager 或編譯錯誤，請提供 Console 最前面的完整錯誤，再進行下一個修正。
