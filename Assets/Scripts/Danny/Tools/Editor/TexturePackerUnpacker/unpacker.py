import os
from PIL import Image
import json

# 封装一个TextureUnpacker类
class TextureUnpacker(object):
    @classmethod
    def split_with_json(cls, f_json, save_dir=None):
        f_json = os.path.abspath(f_json)
        if save_dir is None:
            save_dir = f_json + '_split'
        else:
            save_dir = os.path.abspath(save_dir)
        # 读取json配置表
        f = open(f_json, 'r')
        txt = f.read()
        dt = json.loads(txt)
        f.close()
        # 大图集文件名
        big_texture_file_name = str(dt['meta']['image']).replace(".png", "_png.bytes")

        # 小图序列
        frames = dt['frames']
        # 打开大图
        big_img = Image.open(big_texture_file_name)

        for item in dt["frames"]:
            print(item)

        # 遍历生成小图        
        for key,value in frames.items():            
            info = value
            # 解析配置
            info = cls.parse_as_json(value, key)
            print(info)
            # 小图的保存路径
            little_image_save_path = os.path.join(save_dir, key)
            # 生成小图
            cls.generate_little_image(big_img, info, little_image_save_path)

    @classmethod
    def generate_little_image(cls, big_img, info, path):
        # 创建小图
        little_img = Image.new('RGBA', info['sz'])
        # PIL.Image.crop()方法用于裁剪任何图像的矩形部分
        # box –定义左，上，右和下像素坐标的4元组
        region = big_img.crop(info['box'])
        if info['rotated']:
            region = region.transpose(Image.ROTATE_90)
        # 把裁剪出来的图片粘贴到小图上
        little_img.paste(region, info['xy'])
        save_dir = os.path.dirname(path)
        if not os.path.exists(save_dir):
            os.makedirs(save_dir)
        # 保存
        little_img.save(path)

    @classmethod
    def parse_as_json(cls, info, name):
        """
        "filename": "player_1.png",
        "rotated": false,
        "trimmed": false,
        "sourceSize": { "w": 1, "h": 1 },
        "spriteSourceSize": { "x": 0, "y": 0, "w": 1, "h": 1 },
        "frame": { "x": 1, "y": 1, "w": 1, "h": 1 }
        """
        # 小图宽高
        width = info['sourceSize']['w']
        height = info['sourceSize']['h']
        # 小图矩形信息
        frame = info['frame'] 
        # 是否旋转 (顺时针方向90度)
        rotated = info['rotated']
        if rotated:
            # box 定义左、上、右和下像素坐标的4元组
            box = (frame['x'], frame['y'], 
                    frame['x'] + frame['h'],
                    frame['y'] + frame['w'])
        else:
            box = (frame['x'], frame['y'],
                    frame['x'] + frame['w'],
                    frame['y'] + frame['h'])
        # 图形在小图中的偏移
        x = int((width - frame['w']) / 2)
        y = int((height - frame['h']) / 2)

        return {
            'box': box,
            'rotated': rotated,
            'sz': [width, height],
            'xy': (x, y),
            'filename' : name
        }

if __name__ == '__main__':
    unpacker = TextureUnpacker()
    for root,ds,fs in os.walk("./"):
        for fileName in fs:
            if fileName.endswith('.txt') & os.path.exists(fileName.replace(".txt", "_png.bytes")):
                unpacker.split_with_json(fileName)
    
    print('done')