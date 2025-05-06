using System;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using ReactiveUI;

namespace AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _jsonInput;
    private string _outputMessage;

    public string JsonInput
    {
        get => _jsonInput;
        set => this.RaiseAndSetIfChanged(ref _jsonInput, value);
    }

    public string OutputMessage
    {
        get => _outputMessage;
        set => this.RaiseAndSetIfChanged(ref _outputMessage, value);
    }

    public ICommand ParseCommand { get; }
    public ICommand SaveJsonCommand { get; }
    public MainWindowViewModel()
    {
        ParseCommand = ReactiveCommand.Create(ParseJson);
        SaveJsonCommand = ReactiveCommand.Create(SaveJsonToFile);
    }

    private void ParseJson()
    {
        try
        {
            // 尝试修复 JSON 字符串（如果需要）
            var fixedJson = TryFixJson(JsonInput);

            // 反序列化 JSON 字符串为 Person 对象
            var person = JsonSerializer.Deserialize<Person>(fixedJson, MyJsonContext.Default.Person);

            // 输出解析成功的详细信息
            OutputMessage = $"解析成功：\n" +
                            $"Name = {person.Name}\n" +
                            $"Age = {person.Age}\n" +
                            $"Gender = {person.Gender}\n" +
                            $"Email = {person.Email}\n" +
                            $"Phone = {person.Phone}\n" +
                            $"Address = {person.Address}";
        }
        catch (JsonException ex)
        {
            // 处理 JSON 解析异常
            OutputMessage = "解析失败：无效的 JSON 格式。" + ex.Message;
        }
        catch (Exception ex)
        {
            // 处理其他异常
            OutputMessage = "解析失败：" + ex.Message;
        }
    }



    private string TryFixJson(string input)
    {
        // 替换中文符号
        input = input.Replace('“', '"').Replace('”', '"');
        
        // 简单补全：尝试闭合花括号（不严谨，仅为演示）
        if (!input.Trim().EndsWith("}"))
        {
            input += "}";
        }

        // 可进一步加更复杂修复逻辑
        return input;
    }
    
    private void SaveJsonToFile()
    {
        try
        {
            // 每次调用时都生成新的 Person 数据
            var person = GenerateRandomPerson();

            // 将随机生成的 Person 对象序列化为 JSON 字符串
            var json = JsonSerializer.Serialize(person, MyJsonContext.Default.Person);

            // 保存到文件
            var filePath = "output.json"; // 保存的文件路径
            File.WriteAllText(filePath, json); // 将 JSON 写入文件

            // 输出成功信息
            OutputMessage = $"JSON 已保存到 {filePath}:\n{json}";
        }
        catch (Exception ex)
        {
            // 错误处理
            OutputMessage = "保存失败：" + ex.Message;
        }
    }

    
    
    private Person GenerateRandomPerson()
    {
        var random = new Random();
        return new Person
        {
            Name = "Person" + random.Next(1, 1000), // 随机姓名
            Age = random.Next(18, 100),             // 随机年龄
            Gender = random.Next(0, 2) == 0 ? "男" : "女", // 随机性别
            Email = "user" + random.Next(1000, 9999) + "@example.com", // 随机邮箱
            Phone = "138" + random.Next(10000000, 99999999), // 随机电话号码
            Address = "地址 " + random.Next(1, 100) // 随机地址
        };
    }

}


