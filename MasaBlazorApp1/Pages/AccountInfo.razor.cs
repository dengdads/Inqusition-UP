using BlazorComponent;
using MasaBlazorApp1.Data;
using MasaBlazorApp1.Pages;
using Newtonsoft.Json;
using System;

namespace MasaBlazorApp1.Pages
{
    public partial class AccountInfo
    {
        public string b = "";
        private UserAccountInfo Info = new UserAccountInfo();

        [Inject]
        private IPopupService PopupService { get; set; } = null;

        protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Info = await GetUserAccountInfo();
            }
            StateHasChanged();
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task RefreshOverview()
        {
            await GetUserAccountInfo();
        }

        public async Task<UserAccountInfo> GetUserAccountInfo()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "showMyAccount");
            var token = await localStorage.GetItemAsync<string>("token");
            request.Headers.TryAddWithoutValidation("Authorization", token);
            var response = await Http.SendAsync(request);
            Console.WriteLine(response.StatusCode.ToString());
            var Info = JsonConvert.DeserializeObject<UserAccountInfo>(await response.Content.ReadAsStringAsync());
            return Info;
        }

        public async Task UpdateUserAccount()
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), Http.BaseAddress.OriginalString + "updateMyAccount");
            var token = await localStorage.GetItemAsync<string>("token");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(Info.data));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;
            request.Headers.TryAddWithoutValidation("Authorization", token);
            Console.WriteLine(await content.ReadAsStringAsync());
            var response = await Http.SendAsync(request);
            //var response = await Http.PostAsJsonAsync("/updateMyAccount", Info);
            if (response.IsSuccessStatusCode)
            {
                await PopupService.ConfirmAsync("更新", "上传成功", AlertTypes.Success);
            }
            else
            {
                await PopupService.ConfirmAsync("更新", "上传失败", AlertTypes.Error);
            }
            return;
        }

        public void FightAdd()
        {
            Console.WriteLine("添加");
            Info.data.config.daily.fight.Add(new Fight());
            StateHasChanged();
            return;
        }

        public void FightRemove()
        {
            Console.WriteLine("删除");
            Info.data.config.daily.fight.RemoveAt(Info.data.config.daily.fight.Count - 1);
            StateHasChanged();
            return;
        }

        public class CascaderNode
        {
            public string Value { get; set; }
            public string Label { get; set; }
            public List<CascaderNode> Children { get; set; }
        }

        private string _value;

        private List<CascaderNode> _items = new List<CascaderNode>() {
            new CascaderNode()
            {
                Value = "0",
                Label = "剿灭",
                Children = new List<CascaderNode>() {
                        new CascaderNode{Value="jm",Label="自动导航剿灭"},
                        new CascaderNode{Value="长期委托1",Label="长期委托1"},
                        new CascaderNode{Value="长期委托2",Label="长期委托2"},
                        new CascaderNode{Value="长期委托3",Label="长期委托3"}
                    }
            }, new CascaderNode()
            {
                Value = "1",
                Label = "资源",
                Children = new List<CascaderNode>() {
                        new CascaderNode{ Value = "ap", Label="红票"},
                        new CascaderNode{ Value = "ce", Label="龙门币"},
                        new CascaderNode{ Value = "ls", Label="作战记录"},
                        new CascaderNode{ Value = "ca", Label="技能书"},
                        new CascaderNode{ Value = "sk", Label="碳"}
                    }
            }, new CascaderNode()
            {
                Value = "2",
                Label = "活动",
                Children = new List<CascaderNode>() {
                        new CascaderNode{ Value = "hd", Label="导航后三关"},
                        new CascaderNode{ Value = "hd-1", Label="活动第一关"},
                        new CascaderNode{ Value = "hd-2", Label="活动第二关"},
                        new CascaderNode{ Value = "hd-3", Label="活动第三关"},
                        new CascaderNode{ Value = "hd-4", Label="活动第四关"},
                        new CascaderNode{ Value = "hd-5", Label="活动第五关"},
                        new CascaderNode{ Value = "hd-6", Label="活动第六关"},
                        new CascaderNode{ Value = "hd-7", Label="活动第七关"},
                        new CascaderNode{ Value = "hd-8", Label="活动第八关"},
                        new CascaderNode{ Value = "hd-9", Label="活动第九关"},
                        new CascaderNode{ Value = "hd-10", Label="活动第十关"}
                    }
            }, new CascaderNode()
            {
                Value = "3",
                Label = "主线关卡",
                Children = new List<CascaderNode>() {
                        new CascaderNode{
                            Value = "第一章", Label="第一章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "1-1", Label="1-1"},
                                new CascaderNode{Value = "1-2", Label="1-2"},
                                new CascaderNode{Value = "1-3", Label="1-3"},
                                new CascaderNode{Value = "1-4", Label="1-4"},
                                new CascaderNode{Value = "1-5", Label="1-5"},
                                new CascaderNode{Value = "1-6", Label="1-6"},
                                new CascaderNode{Value = "1-7", Label="1-7"},
                                new CascaderNode{Value = "1-8", Label="1-8"},
                                new CascaderNode{Value = "1-9", Label="1-9"},
                                new CascaderNode{Value = "1-10", Label="1-10"},
                                new CascaderNode{Value = "1-11", Label="1-11"},
                                new CascaderNode{Value = "1-12", Label="1-12"}
                            }
                        },
                        new CascaderNode{
                            Value = "第二章", Label="第二章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "2-1", Label="2-1"},
                                new CascaderNode{Value = "s2-1", Label="s2-1"},
                                new CascaderNode{Value = "2-2", Label="2-2"},
                                new CascaderNode{Value = "s2-2", Label="s2-2"},
                                new CascaderNode{Value = "2-3", Label="2-3"},
                                new CascaderNode{Value = "s2-3", Label="s2-3"},
                                new CascaderNode{Value = "2-4", Label="2-4"},
                                new CascaderNode{Value = "s2-4", Label="s2-4"},
                                new CascaderNode{Value = "2-5", Label="2-5"},
                                new CascaderNode{Value = "s2-5", Label="s2-5"},
                                new CascaderNode{Value = "2-6", Label="2-6"},
                                new CascaderNode{Value = "s2-6", Label="s2-6"},
                                new CascaderNode{Value = "2-7", Label="2-7"},
                                new CascaderNode{Value = "s2-7", Label="s2-7"},
                                new CascaderNode{Value = "2-8", Label="2-8"},
                                new CascaderNode{Value = "s2-8", Label="s2-8"},
                                new CascaderNode{Value = "2-9", Label="2-9"},
                                new CascaderNode{Value = "s2-9", Label="s2-9"},
                                new CascaderNode{Value = "2-10", Label="2-10"},
                                new CascaderNode{Value = "s2-10", Label="s2-10"},
                                new CascaderNode{Value = "s2-11", Label="s2-11"},
                                new CascaderNode{Value = "s2-12", Label="s2-12"}
                            }
                        },
                        new CascaderNode{
                            Value = "第三章", Label="第三章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "3-1", Label="3-1"},
                                new CascaderNode{Value = "s3-1", Label="s3-1"},
                                new CascaderNode{Value = "3-2", Label="3-2"},
                                new CascaderNode{Value = "s3-2", Label="s3-2"},
                                new CascaderNode{Value = "3-3", Label="3-3"},
                                new CascaderNode{Value = "s3-3", Label="s3-3"},
                                new CascaderNode{Value = "3-4", Label="3-4"},
                                new CascaderNode{Value = "s3-4", Label="s3-4"},
                                new CascaderNode{Value = "3-5", Label="3-5"},
                                new CascaderNode{Value = "s3-5", Label="s3-5"},
                                new CascaderNode{Value = "3-6", Label="3-6"},
                                new CascaderNode{Value = "s3-6", Label="s3-6"},
                                new CascaderNode{Value = "3-7", Label="3-7"},
                                new CascaderNode{Value = "s3-7", Label="s3-7"},
                                new CascaderNode{Value = "3-8", Label="3-8"}
                            }
                        },
                        new CascaderNode{
                            Value = "第四章", Label="第四章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "4-1", Label="4-1"},
                                new CascaderNode{Value = "s4-1", Label="s4-1"},
                                new CascaderNode{Value = "4-2", Label="4-2"},
                                new CascaderNode{Value = "s4-2", Label="s4-2"},
                                new CascaderNode{Value = "4-3", Label="4-3"},
                                new CascaderNode{Value = "s4-3", Label="s4-3"},
                                new CascaderNode{Value = "4-4", Label="4-4"},
                                new CascaderNode{Value = "s4-4", Label="s4-4"},
                                new CascaderNode{Value = "4-5", Label="4-5"},
                                new CascaderNode{Value = "s4-5", Label="s4-5"},
                                new CascaderNode{Value = "4-6", Label="4-6"},
                                new CascaderNode{Value = "s4-6", Label="s4-6"},
                                new CascaderNode{Value = "4-7", Label="4-7"},
                                new CascaderNode{Value = "s4-7", Label="s4-7"},
                                new CascaderNode{Value = "4-8", Label="4-8"},
                                new CascaderNode{Value = "s4-8", Label="s4-8"},
                                new CascaderNode{Value = "4-9", Label="4-9"},
                                new CascaderNode{Value = "s4-9", Label="s4-9"},
                                new CascaderNode{Value = "4-10", Label="4-10"},
                                new CascaderNode{Value = "s4-10", Label="s4-10"},
                            }
                        },
                        new CascaderNode{
                            Value = "第五章", Label="第五章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "5-1", Label="5-1"},
                                new CascaderNode{Value = "s5-1", Label="52-1"},
                                new CascaderNode{Value = "5-2", Label="5-2"},
                                new CascaderNode{Value = "s5-2", Label="s5-2"},
                                new CascaderNode{Value = "5-3", Label="5-3"},
                                new CascaderNode{Value = "s5-3", Label="s5-3"},
                                new CascaderNode{Value = "5-4", Label="5-4"},
                                new CascaderNode{Value = "s5-4", Label="s5-4"},
                                new CascaderNode{Value = "5-5", Label="5-5"},
                                new CascaderNode{Value = "s5-5", Label="s5-5"},
                                new CascaderNode{Value = "5-6", Label="5-6"},
                                new CascaderNode{Value = "s5-6", Label="s5-6"},
                                new CascaderNode{Value = "5-7", Label="5-7"},
                                new CascaderNode{Value = "s5-7", Label="s5-7"},
                                new CascaderNode{Value = "5-8", Label="5-8"},
                                new CascaderNode{Value = "s5-8", Label="s5-8"},
                                new CascaderNode{Value = "5-9", Label="5-9"},
                                new CascaderNode{Value = "s5-9", Label="s5-9"},
                                new CascaderNode{Value = "5-10", Label="5-10"}
                            }
                        },
                        new CascaderNode{
                            Value = "第六章", Label="第六章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "6-1", Label="6-1"},
                                new CascaderNode{Value = "h6-1", Label="h6-1"},
                                new CascaderNode{Value = "6-2", Label="6-2"},
                                new CascaderNode{Value = "h6-2", Label="h6-2"},
                                new CascaderNode{Value = "6-3", Label="6-3"},
                                new CascaderNode{Value = "h6-3", Label="h6-3"},
                                new CascaderNode{Value = "6-4", Label="6-4"},
                                new CascaderNode{Value = "h6-4", Label="h6-4"},
                                new CascaderNode{Value = "6-5", Label="6-5"},
                                new CascaderNode{Value = "6-7", Label="6-7"},
                                new CascaderNode{Value = "6-8", Label="6-8"},
                                new CascaderNode{Value = "6-9", Label="6-9"},
                                new CascaderNode{Value = "6-10", Label="6-10"},
                                new CascaderNode{Value = "6-11", Label="6-11"},
                                new CascaderNode{Value = "6-12", Label="6-12"},
                                new CascaderNode{Value = "6-14", Label="6-14"},
                                new CascaderNode{Value = "6-15", Label="6-15"},
                                new CascaderNode{Value = "6-16", Label="6-16"},
                                new CascaderNode{Value = "6-17", Label="6-17"}
                            }
                        },
                        new CascaderNode{
                            Value = "第七章", Label="第七章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "h7-1", Label="h7-1"},
                                new CascaderNode{Value = "7-2", Label="7-2"},
                                new CascaderNode{Value = "h7-2", Label="h7-2"},
                                new CascaderNode{Value = "7-3", Label="7-3"},
                                new CascaderNode{Value = "h7-3", Label="h7-3"},
                                new CascaderNode{Value = "7-4", Label="7-4"},
                                new CascaderNode{Value = "h7-4", Label="h7-4"},
                                new CascaderNode{Value = "7-5", Label="7-5"},
                                new CascaderNode{Value = "7-6", Label="7-6"},
                                new CascaderNode{Value = "7-8", Label="7-8"},
                                new CascaderNode{Value = "7-9", Label="7-9"},
                                new CascaderNode{Value = "7-10", Label="7-10"},
                                new CascaderNode{Value = "7-11", Label="7-11"},
                                new CascaderNode{Value = "7-12", Label="7-12"},
                                new CascaderNode{Value = "7-13", Label="7-13"},
                                new CascaderNode{Value = "7-14", Label="7-14"},
                                new CascaderNode{Value = "7-15", Label="7-15"},
                                new CascaderNode{Value = "7-16", Label="7-16"},
                                new CascaderNode{Value = "7-17", Label="7-17"},
                                new CascaderNode{Value = "7-18", Label="7-18"}
                            }
                        },
                        new CascaderNode{
                            Value = "第八章", Label="第八章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "r8-1", Label="r8-1"},
                                new CascaderNode{Value = "h8-1", Label="h8-1"},
                                new CascaderNode{Value = "jt8-1", Label="jt8-1"},
                                new CascaderNode{Value = "r8-2", Label="r8-2"},
                                new CascaderNode{Value = "h8-2", Label="h8-2"},
                                new CascaderNode{Value = "jt8-2", Label="jt8-2"},
                                new CascaderNode{Value = "r8-3", Label="r8-3"},
                                new CascaderNode{Value = "h8-3", Label="h8-3"},
                                new CascaderNode{Value = "jt8-3", Label="jt8-3"},
                                new CascaderNode{Value = "8-4", Label="8-4"},
                                new CascaderNode{Value = "h8-4", Label="h8-4"},
                                new CascaderNode{Value = "r8-5", Label="r8-5"},
                                new CascaderNode{Value = "r8-6", Label="r8-6"},
                                new CascaderNode{Value = "m8-6", Label="m8-6"},
                                new CascaderNode{Value = "r8-7", Label="r8-7"},
                                new CascaderNode{Value = "m8-7", Label="m8-7"},
                                new CascaderNode{Value = "r8-8", Label="r8-8"},
                                new CascaderNode{Value = "m8-8", Label="m8-8"},
                                new CascaderNode{Value = "r8-9", Label="r8-9"},
                                new CascaderNode{Value = "r8-10", Label="r8-10"},
                                new CascaderNode{Value = "r8-11", Label="r8-11"}
                            }
                        },
                        new CascaderNode{
                            Value = "第九章", Label="第九章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "s9-1", Label="s9-1"},
                                new CascaderNode{Value = "h9-1", Label="h9-1"},
                                new CascaderNode{Value = "9-2", Label="9-2"},
                                new CascaderNode{Value = "h9-2", Label="h9-2"},
                                new CascaderNode{Value = "9-3", Label="9-3"},
                                new CascaderNode{Value = "h9-3", Label="h9-3"},
                                new CascaderNode{Value = "9-4", Label="9-4"},
                                new CascaderNode{Value = "h9-4", Label="h9-4"},
                                new CascaderNode{Value = "9-5", Label="9-5"},
                                new CascaderNode{Value = "h9-5", Label="h9-5"},
                                new CascaderNode{Value = "9-6", Label="9-6"},
                                new CascaderNode{Value = "h9-6", Label="h9-6"},
                                new CascaderNode{Value = "9-7", Label="9-7"},
                                new CascaderNode{Value = "9-8", Label="9-8"},
                                new CascaderNode{Value = "9-9", Label="9-9"},
                                new CascaderNode{Value = "9-10", Label="9-10"},
                                new CascaderNode{Value = "9-11", Label="9-11"},
                                new CascaderNode{Value = "9-12", Label="9-12"},
                                new CascaderNode{Value = "9-13", Label="9-13"},
                                new CascaderNode{Value = "9-14", Label="9-14"},
                                new CascaderNode{Value = "9-15", Label="9-15"},
                                new CascaderNode{Value = "9-16", Label="9-16"},
                                new CascaderNode{Value = "9-17", Label="9-17"},
                                new CascaderNode{Value = "9-18", Label="9-18"},
                                new CascaderNode{Value = "9-19", Label="9-19"}
                            }
                        },
                        new CascaderNode{
                            Value = "第十章", Label="第十章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "h10-1", Label="h10-1"},
                                new CascaderNode{Value = "10-2", Label="10-2"},
                                new CascaderNode{Value = "h10-2", Label="h10-2"},
                                new CascaderNode{Value = "10-3", Label="10-3"},
                                new CascaderNode{Value = "h10-3", Label="h10-3"},
                                new CascaderNode{Value = "10-4", Label="10-4"},
                                new CascaderNode{Value = "10-5", Label="10-5"},
                                new CascaderNode{Value = "10-6", Label="10-6"},
                                new CascaderNode{Value = "10-7", Label="10-7"},
                                new CascaderNode{Value = "10-8", Label="10-8"},
                                new CascaderNode{Value = "10-9", Label="10-9"},
                                new CascaderNode{Value = "10-10", Label="10-10"},
                                new CascaderNode{Value = "10-11", Label="10-11"},
                                new CascaderNode{Value = "10-12", Label="10-12"},
                                new CascaderNode{Value = "10-14", Label="10-14"},
                                new CascaderNode{Value = "10-15", Label="10-15"},
                                new CascaderNode{Value = "10-16", Label="10-16"},
                                new CascaderNode{Value = "10-17", Label="10-17"}
                            }
                        },
                        new CascaderNode{
                            Value = "第十一章", Label="第十一章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "h11-1", Label="h11-1"},
                                new CascaderNode{Value = "11-2", Label="11-2"},
                                new CascaderNode{Value = "h11-2", Label="h11-2"},
                                new CascaderNode{Value = "11-3", Label="11-3"},
                                new CascaderNode{Value = "h11-3", Label="h11-3"},
                                new CascaderNode{Value = "h11-4", Label="h11-4"},
                                new CascaderNode{Value = "11-5", Label="11-5"},
                                new CascaderNode{Value = "11-6", Label="11-6"},
                                new CascaderNode{Value = "11-7", Label="11-7"},
                                new CascaderNode{Value = "11-8", Label="11-8"},
                                new CascaderNode{Value = "11-9", Label="11-9"},
                                new CascaderNode{Value = "11-11", Label="11-11"},
                                new CascaderNode{Value = "11-12", Label="11-12"},
                                new CascaderNode{Value = "11-13", Label="11-13"},
                                new CascaderNode{Value = "11-14", Label="11-14"},
                                new CascaderNode{Value = "11-15", Label="11-15"},
                                new CascaderNode{Value = "11-16", Label="11-16"},
                                new CascaderNode{Value = "11-17", Label="11-17"},
                                new CascaderNode{Value = "11-18", Label="11-18"},
                                new CascaderNode{Value = "11-19", Label="11-19"}
                            }
                        },
                        new CascaderNode{
                            Value = "第十二章", Label="第十二章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "h12-1", Label="h12-1"},
                                new CascaderNode{Value = "12-2", Label="12-2"},
                                new CascaderNode{Value = "h12-2", Label="h12-2"},
                                new CascaderNode{Value = "12-3", Label="12-3"},
                                new CascaderNode{Value = "h12-3", Label="h12-3"},
                                new CascaderNode{Value = "12-4", Label="12-4"},
                                new CascaderNode{Value = "h12-4", Label="h12-4"},
                                new CascaderNode{Value = "12-5", Label="12-5"},
                                new CascaderNode{Value = "12-6", Label="12-6"},
                                new CascaderNode{Value = "12-7", Label="12-7"},
                                new CascaderNode{Value = "12-8", Label="12-8"},
                                new CascaderNode{Value = "12-9", Label="12-9"},
                                new CascaderNode{Value = "12-10", Label="12-10"},
                                new CascaderNode{Value = "12-12", Label="12-12"},
                                new CascaderNode{Value = "12-13", Label="12-13"},
                                new CascaderNode{Value = "12-14", Label="12-14"},
                                new CascaderNode{Value = "12-15", Label="12-15"},
                                new CascaderNode{Value = "12-16", Label="12-16"},
                                new CascaderNode{Value = "12-17", Label="12-17"},
                                new CascaderNode{Value = "12-18", Label="12-18"},
                                new CascaderNode{Value = "12-19", Label="12-19"},
                                new CascaderNode{Value = "12-20", Label="12-20"}
                            }
                        },
                        new CascaderNode{
                            Value = "第十三章", Label="第十三章",Children=new List<CascaderNode>(){
                                new CascaderNode{Value = "13-2", Label="13-2"},
                                new CascaderNode{Value = "13-3", Label="13-3"},
                                new CascaderNode{Value = "13-4", Label="13-4"},
                                new CascaderNode{Value = "13-5", Label="13-5"},
                                new CascaderNode{Value = "13-6", Label="13-6"},
                                new CascaderNode{Value = "13-7", Label="13-7"},
                                new CascaderNode{Value = "13-8", Label="13-8"},
                                new CascaderNode{Value = "13-10", Label="13-10"},
                                new CascaderNode{Value = "13-11", Label="13-11"},
                                new CascaderNode{Value = "13-12", Label="13-12"},
                                new CascaderNode{Value = "13-13", Label="13-13"},
                                new CascaderNode{Value = "13-14", Label="13-14"},
                                new CascaderNode{Value = "13-15", Label="13-15"},
                                new CascaderNode{Value = "13-16", Label="13-16"},
                                new CascaderNode{Value = "13-17", Label="13-17"},
                                new CascaderNode{Value = "13-18", Label="13-18"},
                                new CascaderNode{Value = "13-19", Label="13-19"},
                                new CascaderNode{Value = "13-20", Label="13-20"},
                                new CascaderNode{Value = "13-21", Label="13-21"},
                            }
                        },
                    }
            }, new CascaderNode()
            {
                Value = "4",
                Label = "插曲",
                Children = new List<CascaderNode>() {
                        new CascaderNode{ Value = "孤星", Label="孤星", Children = new List<CascaderNode>(){
                            new CascaderNode{ Value = "cw-6", Label="cw-6"}
                        } },
                    }
            }, new CascaderNode()
            {
                Value = "5",
                Label = "芯片",
                Children = new List<CascaderNode>() {
                        new CascaderNode{ Value = "pr-a-1", Label="重/医芯片" },
                        new CascaderNode{ Value = "pr-a-2", Label="重/医芯片组" },
                        new CascaderNode{ Value = "pr-b-1", Label="狙/术芯片" },
                        new CascaderNode{ Value = "pr-b-2", Label="狙/术芯片组" },
                        new CascaderNode{ Value = "pr-c-1", Label="先/辅芯片" },
                        new CascaderNode{ Value = "pr-c-2", Label="先/辅芯片组" },
                        new CascaderNode{ Value = "pr-d-1", Label="近/特芯片" },
                        new CascaderNode{ Value = "pr-d-2", Label="近/特芯片组" },
                    }
            }, new CascaderNode()
            {
                Value = "6",
                Label = "材料",
                Children = new List<CascaderNode>() {
                        new CascaderNode{ Value = "扭转醇", Label="扭转醇" },
                        new CascaderNode{ Value = "轻锰矿", Label="轻锰矿" },
                        new CascaderNode{ Value = "RMA70-12", Label="RMA70-12" },
                        new CascaderNode{ Value = "固源岩组", Label="固源岩组" },
                        new CascaderNode{ Value = "固源岩", Label="固源岩" },
                        new CascaderNode{ Value = "研磨石", Label="研磨石" },
                        new CascaderNode{ Value = "全新装置", Label="全新装置" },
                        new CascaderNode{ Value = "装置", Label="装置" },
                        new CascaderNode{ Value = "聚酸酯组", Label="聚酸酯组" },
                        new CascaderNode{ Value = "聚酸酯", Label="聚酸酯" },
                        new CascaderNode{ Value = "糖组", Label="糖组" },
                        new CascaderNode{ Value = "糖", Label="糖" },
                        new CascaderNode{ Value = "异铁组", Label="异铁组" },
                        new CascaderNode{ Value = "异铁", Label="异铁" },
                        new CascaderNode{ Value = "酮凝集组", Label="酮凝集组" },
                        new CascaderNode{ Value = "酮凝集", Label="酮凝集" },
                        new CascaderNode{ Value = "凝胶", Label="凝胶" },
                        new CascaderNode{ Value = "炽合金", Label="炽合金" },
                        new CascaderNode{ Value = "晶体元件", Label="晶体元件" },
                        new CascaderNode{ Value = "半自然溶剂", Label="半自然溶剂" },
                        new CascaderNode{ Value = "化合切削液", Label="化合切削液" },
                        new CascaderNode{ Value = "转质盐组", Label="转质盐组" },
                    }
            }
        };
    }
}